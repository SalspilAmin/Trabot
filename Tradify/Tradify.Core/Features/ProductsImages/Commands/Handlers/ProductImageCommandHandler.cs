using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.ProductsImages.Commands.Models;
using Tradify.Core.Features.ProductVariantsImages.Commands.Models;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;
using static Tradify.Data.AppMetaData.Router;


namespace Tradify.Core.Features.ProductsImages.Commands.Handlers
{
    public  class ProductImageCommandHandler : ResponseHandler
        ,IRequestHandler<AddProductImageCommand, Response<string>>
        , IRequestHandler<UpdateProductImageCommand, Response<string>>
        , IRequestHandler<DeleteProductImageCommand, Response<string>>


    {

        #region Fields
        private readonly LocalizationService localize;
        private readonly IProductService productService;
        private readonly IFileService fileService;
        private readonly IProductImageService productImageService;
        private readonly IMapper mapper;
        private readonly ICurrentUserService currentUserService;
        private readonly ISellerService sellerService;


        #endregion

        #region Constructor
        public ProductImageCommandHandler(IProductService productService,
                                     IMapper mapper,
                                     IProductImageService productImageService,
                                     IFileService fileService,
                                     ICurrentUserService currentUserService,
                                     LocalizationService localize,
                                     ISellerService sellerService) : base(localize)
        {
            this.productService = productService;
            this.mapper = mapper;
            this.fileService = fileService;
            this.productImageService = productImageService;
            this.localize = localize;
            this.currentUserService = currentUserService;
            this.sellerService = sellerService; 
        }
        #endregion
      
        #region Methods
        public async Task<Response<string>> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
        {
            // 1️⃣ Get current user
            var currentUserId = currentUserService.GetUserId();
            var seller = await sellerService.GetTableNoTracking()
                .FirstOrDefaultAsync(s => s.UserId == currentUserId);

            if (seller == null)
                return NotFound<string>(localize.Get("SellerNotFound"));
        

            // 2️⃣ Get product
            var product = await productService.GetTableAsTracking()
                     .Include(p => p.Store)
                     .Where(p => p.Id == request.ProductId && p.Store.SellerId == seller.Id)
                     .FirstOrDefaultAsync();
            if (product == null)
                return NotFound<string>(localize.Get("ProductNotFound"));
            // Cheack if Product Has Main Image 
            if (request.IsMain)
            {
                var mainImage = await productImageService.GetTableNoTracking()
                  .FirstOrDefaultAsync(i => i.ProductId == product.Id && i.IsMain);

                if (mainImage != null)
                    return BadRequest<string>(localize.Get("ProductAlreadyHasMainImage"));
            }
            int sortOrder = 1;
            if (!request.IsMain)
            {
                var lastSort = await productImageService.GetTableNoTracking()
                                    .Where(i => i.ProductId == request.ProductId)
                                    .MaxAsync(i => (int?)i.SortOrder) ?? 0;

                sortOrder = lastSort + 1;
                
            }


            // 3️⃣ Upload image

            var folderName = $"{UploadFolder.Products}/{request.ProductId}";

            var uploadResult = await fileService.UploadImageAsync(
                         request.Image,
                         folderName);

            if (uploadResult.Error != "Success")
            {
                return BadRequest<string>(localize.Get(uploadResult.Error));
            }


            // 5️⃣ Save in DB
            var productimage = new Data.Entities.ProductImage
            {
                ProductId = request.ProductId,
                IsMain = request.IsMain,    
                SortOrder = sortOrder,
                MediaPath = uploadResult.Url,
                PublicId = uploadResult.PublicId
            };

            

            await productImageService.AddAsync(productimage);
            await productImageService.SaveChangesAsync();

            return Success(localize.Get("ImageAddedSuccessfully"));

        }





        // Update product varint image 

        #region Update product varint image
        public async Task<Response<string>> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = currentUserService.GetUserId();
            var seller = await sellerService.GetTableNoTracking()
                .FirstOrDefaultAsync(s => s.UserId == currentUserId);
            if (seller == null)
                return Unauthorized<string>(localize.Get("SellerNotFound"));
            // 1 Get image
            var image = await productImageService.GetTableAsTracking()
     .Include(i => i.Product)
     .ThenInclude(p => p.Store)
     .FirstOrDefaultAsync(i =>
         i.Id == request.ImageId &&
         i.Product.Store.SellerId == seller.Id);


            if (image == null)
                return NotFound<string>(localize.Get("ImageNotFound"));

            // Upload New Image

            var folderName = $"{UploadFolder.Products}/{image.ProductId}";

            var uploadResult = await fileService.UploadImageAsync(
                         request.Image,
                         folderName);

            if (uploadResult.Error != "Success")
            {
                return BadRequest<string>(localize.Get(uploadResult.Error));
            }


            //  Delete image

            if (!string.IsNullOrWhiteSpace(image.PublicId))
            {
                await fileService.DeleteImageAsync(image.PublicId);
            }


            // Update Database
            image.MediaPath = uploadResult.Url;
            image.PublicId = uploadResult.PublicId;


            await productImageService.UpdateAsync(image);
            await productImageService.SaveChangesAsync();

            return Success<string>(localize.Get("ImageUpdatedSuccessfully"));
        }

        #endregion

        //Delete product  Image 

        #region Delete product  Image 
        public async Task<Response<string>> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = currentUserService.GetUserId();
            var seller = await sellerService.GetTableNoTracking()
                .FirstOrDefaultAsync(s => s.UserId == currentUserId);
            if (seller == null)
                return Unauthorized<string>(localize.Get("SellerNotFound"));
            // 1 Get image
            var image = await productImageService.GetTableAsTracking()
                            .Include(i => i.Product)
                          .ThenInclude(p => p.Store)
                        .FirstOrDefaultAsync(i => i.Id == request.Id
                        && i.Product.Store.SellerId == seller.Id);

            if (image == null)
                return NotFound<string>(localize.Get("ImageNotFound"));

            if (image.IsMain)
            {
                var nextImage = await productImageService.GetTableAsTracking()
                    .Where(i => i.ProductId == image.ProductId && i.Id != image.Id)
                    .OrderBy(i => i.SortOrder)
                    .FirstOrDefaultAsync();

                if (nextImage != null)
                {
                    nextImage.IsMain = true;
                }
            }

            var images = await productImageService.GetTableAsTracking()
                .Where(i => i.ProductId == image.ProductId && i.Id != image.Id)
                .OrderByDescending(i => i.IsMain) 
                .ThenBy(i => i.SortOrder)
                .ToListAsync();

            int order = 1;

            foreach (var item in images)
            {
                item.SortOrder = order++;
            }

            // 3️⃣ Delete image

            if (!string.IsNullOrWhiteSpace(image.PublicId))
            {
                await fileService.DeleteImageAsync(image.PublicId);
            }



            await productImageService.DeleteAsync(image);
            await productImageService.SaveChangesAsync();

            return Success<string>(localize.Get("ImageDeletedSuccessfully"));
        }

        #endregion





        #endregion
    }
}


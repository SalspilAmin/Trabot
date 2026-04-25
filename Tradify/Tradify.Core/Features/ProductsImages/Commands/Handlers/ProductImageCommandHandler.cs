using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.ProductsImages.Commands.Models;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;


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


        #endregion

        #region Constructor
        public ProductImageCommandHandler(IProductService productService,
                                     IMapper mapper,
                                     IProductImageService productImageService,
                                     IFileService fileService,
                                     ICurrentUserService currentUserService,
                                     LocalizationService localize) : base(localize)
        {
            this.productService = productService;
            this.mapper = mapper;
            this.fileService = fileService;
            this.productImageService = productImageService;
            this.localize = localize;
            this.currentUserService = currentUserService;
        }
        #endregion
       



        #region Methods
        public async Task<Response<string>> Handle(AddProductImageCommand request, CancellationToken cancellationToken)
        {
            

         

         
            // 1️⃣ Get current user
            var currentUserId = currentUserService.GetUserId();

            // 2️⃣ Get product
            var product = await productService.GetTableAsTracking()
                     .Include(p => p.Store)
                     .Where(p => p.Id == request.ProductId && p.Store.SellerId == currentUserId)
                     .FirstOrDefaultAsync();
            if (product == null)
                return NotFound<string>(localize.Get("ProductNotFound"));



            // 3️⃣ Upload image
            var imagePath = await fileService.UploadGenericAsync(
            UploadFolder.Products,
            request.ProductId,
            request.Image
        );
            if (imagePath == "NoFile"||imagePath=="InvalidImageType")
            {
                return BadRequest<string>(imagePath);
            }

          
              
            // 5️⃣ Save in DB
            var productImage = new ProductImage
            {
                ProductId = request.ProductId,
                MediaPath = imagePath,
                IsMain =true,
                SortOrder = request.SortOrder
            };
            



            await productImageService.AddAsync(productImage);
            await productImageService.SaveChangesAsync();

            return Success(localize.Get("ImageAddedSuccessfully"));

        }


     

        public async Task<Response<string>> Handle(UpdateProductImageCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = currentUserService.GetUserId();

            // 1️⃣ Get image
            var image = await productImageService.GetTableAsTracking()
                .Include(i => i.Product)
                .ThenInclude(p => p.Store)
                .FirstOrDefaultAsync(i => i.Id == request.ImageId
                && i.Product.Store.SellerId == currentUserId);

            if (image == null)
                return NotFound<string>(localize.Get("ImageNotFound"));

            
            // 3️⃣ Update main image
            if (request.IsMain && !image.IsMain)
            {
                var oldMainImages = await productImageService.GetTableAsTracking()
                    .Where(i => i.ProductId == image.ProductId && i.IsMain)
                     .ExecuteUpdateAsync(setters =>
                        setters.SetProperty(i => i.IsMain, false));
            }

            // 4️⃣ Update fields
            image.IsMain = request.IsMain;
            image.SortOrder = request.SortOrder;

            await productImageService.UpdateAsync(image);
            await productImageService.SaveChangesAsync();

            return Success<string>(localize.Get("ImageUpdatedSuccessfully"));
        }

        public async Task<Response<string>> Handle(DeleteProductImageCommand request, CancellationToken cancellationToken)
        {
            // 1️⃣ Get image with related product
            var currentUserId = currentUserService.GetUserId();
            var image = await productImageService.GetTableAsTracking()
                .Include(i => i.Product)
                .ThenInclude(p => p.Store)
                .FirstOrDefaultAsync(i => i.Id == request.Id 
                && i.Product.Store.SellerId == currentUserId, cancellationToken);

            if (image == null)
                return NotFound<string>(localize.Get("ImageNotFound"));

            if (image.IsMain)
            {
                var anotherImage = await productImageService.GetTableAsTracking()
                    .FirstOrDefaultAsync(i => i.ProductId == image.ProductId && i.Id != image.Id);

                if (anotherImage != null)
                    anotherImage.IsMain = true;
            }
            // 3️⃣ Delete image
            if (!string.IsNullOrWhiteSpace(image.MediaPath))
            {
                await fileService.DeleteFile(image.MediaPath);
            }
             await productImageService.DeleteAsync(image);
            await productImageService.SaveChangesAsync();

            return Success<string>(localize.Get("ImageDeletedSuccessfully"));
        }
   
        
        


        #endregion
    }
}


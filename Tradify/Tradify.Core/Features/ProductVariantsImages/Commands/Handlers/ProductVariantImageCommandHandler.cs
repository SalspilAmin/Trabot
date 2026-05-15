using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.ProductVariantsImages.Commands.Models;
using Tradify.Core.Features.ProductVariantsImages.Commands.Models;
using Tradify.Core.Features.StoreImage.Commands.Models;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;
using static Tradify.Data.AppMetaData.Router;


namespace Tradify.Core.Features.ProductVariantsImages.Commands.Handlers
{
    public class ProductVariantImageCommandHandler : ResponseHandler
        , IRequestHandler<AddProductVariantImageCommand, Response<string>>
    , IRequestHandler<UpdateProductVariantImageCommand, Response<string>>
    , IRequestHandler<DeleteProductVariantImageCommand, Response<string>>


    {

        #region Fields
        private readonly LocalizationService localize;
        private readonly IProductVariantService productVariantService;
        private readonly IFileService fileService;
        private readonly IProductVariantImageService productVariantImageService;
        private readonly IMapper mapper;
        private readonly ICurrentUserService currentUserService;
        private readonly ISellerService sellerService;


        #endregion

        #region Constructor
        public ProductVariantImageCommandHandler(IProductVariantService productVariantService,
                                     IMapper mapper,
                                     IProductVariantImageService productVariantImageService,
                                     IFileService fileService,
                                     ICurrentUserService currentUserService,
                                     LocalizationService localize, ISellerService sellerService) : base(localize)
        {
            this.productVariantService = productVariantService;
            this.mapper = mapper;
            this.fileService = fileService;
            this.productVariantImageService = productVariantImageService;
            this.localize = localize;
            this.currentUserService = currentUserService;
            this.sellerService = sellerService;
        }
        #endregion

        #region Methods

        //Add Product VArint Image 
        #region Add Product VArint Image 
        public async Task<Response<string>> Handle(AddProductVariantImageCommand request, CancellationToken cancellationToken)
        {
            // 1️⃣ Get current user
            var currentUserId = currentUserService.GetUserId();
            var seller = await sellerService.GetTableNoTracking().FirstOrDefaultAsync(s => s.UserId == currentUserId);

            if (seller == null)
                return NotFound<string>(localize.Get("SellerNotFound"));
            // 2️⃣ Get productVariant
            var productVariant = await productVariantService.GetTableAsTracking()
                     .Include(p => p.Product)
                     .ThenInclude(p => p.Store)
                     .FirstOrDefaultAsync(p => p.Id == request.ProductVariantId &&
                                          p.Product.Store.SellerId == seller.Id);
            if (productVariant == null)
                return NotFound<string>(localize.Get("ProductVariantNotFound"));


            // has already image


            if (productVariant.ProductVariantImage != null)
                return BadRequest<string>(localize.Get("ProductVarianHasImageAlrady"));


            // 3️⃣ Upload image

            var folderName = $"{UploadFolder.Variants}/{request.ProductVariantId}";

            var uploadResult = await fileService.UploadImageAsync(
                         request.Image,
                         folderName);

            if (uploadResult.Error != "Success")
            {
                return BadRequest<string>(localize.Get(uploadResult.Error));
            }


            // 5️⃣ Save in DB
            var varintImage = new Data.Entities.ProductVariantImage
            {
                ProductVariantId = request.ProductVariantId,
                MediaPath = uploadResult.Url,
                PublicId = uploadResult.PublicId
            };


            await productVariantImageService.AddAsync(varintImage);
            await productVariantImageService.SaveChangesAsync();

            return Success(localize.Get("ImageAddedSuccessfully"));


        }
        #endregion

        // Update product varint image 

        #region Update product varint image
        public async Task<Response<string>> Handle(UpdateProductVariantImageCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = currentUserService.GetUserId();
            var seller = await sellerService.GetTableNoTracking()
                .FirstOrDefaultAsync(s => s.UserId == currentUserId);
            if (seller == null)
                return Unauthorized<string>(localize.Get("SellerNotFound"));
            // 1 Get image
            var image = await productVariantImageService.GetTableAsTracking()
                        .FirstOrDefaultAsync(i => i.Id == request.ImageId
                        && i.ProductVariant.Product.Store.SellerId == seller.Id);


            if (image == null)
                return NotFound<string>(localize.Get("ImageNotFound"));

            // Upload New Image

            var folderName = $"{UploadFolder.Variants}/{image.ProductVariantId}";

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


            await productVariantImageService.UpdateAsync(image);
            await productVariantImageService.SaveChangesAsync();

            return Success<string>(localize.Get("ImageUpdatedSuccessfully"));
        }

        #endregion

        //Delete product varint Image 

        #region Delete product varint Image 
        public async Task<Response<string>> Handle(DeleteProductVariantImageCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = currentUserService.GetUserId();
            var seller = await sellerService.GetTableNoTracking()
                .FirstOrDefaultAsync(s => s.UserId == currentUserId);
            if (seller == null)
                return Unauthorized<string>(localize.Get("SellerNotFound"));
            // 1 Get image
            var image = await productVariantImageService.GetTableAsTracking()
                        .FirstOrDefaultAsync(i => i.Id == request.Id
                        && i.ProductVariant.Product.Store.SellerId == seller.Id);


            if (image == null)
                return NotFound<string>(localize.Get("ImageNotFound"));


            // 3️⃣ Delete image

            if (!string.IsNullOrWhiteSpace(image.PublicId))
            {
                await fileService.DeleteImageAsync(image.PublicId);
            }



            await productVariantImageService.DeleteAsync(image);
            await productVariantImageService.SaveChangesAsync();

            return Success<string>(localize.Get("ImageDeletedSuccessfully"));
        }

        #endregion


        #endregion
    }
}


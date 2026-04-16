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
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;


namespace Tradify.Core.Features.ProductVariantsImages.Commands.Handlers
{
    public class ProductVariantImageCommandHandler : ResponseHandler
        , IRequestHandler<AddProductVariantImageCommand, Response<string>>
        //, IRequestHandler<UpdateProductVariantImageCommand, Response<string>>
        //, IRequestHandler<DeleteProductVariantImageCommand, Response<string>>


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
                                     LocalizationService localize , ISellerService sellerService) : base(localize)
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
        public async Task<Response<string>> Handle(AddProductVariantImageCommand request, CancellationToken cancellationToken)
        {
            // 1️⃣ Get current user
            var currentUserId = currentUserService.GetUserId();
            var seller = await sellerService.GetTableNoTracking().FirstOrDefaultAsync(s => s.UserId == currentUserId);

            // var seller = await sellerService.GetTableNoTracking().FirstOrDefaultAsync(s=>s.Id== request.SellerId);
            if (seller==null)
                return NotFound<string>(localize.Get("SellerNotFound"));
            // 2️⃣ Get productVariant
            var productVariant = await productVariantService.GetTableAsTracking()
                     .Include(p => p.Product)
                     .ThenInclude(p => p.Store)
                     .FirstOrDefaultAsync(p => p.Id == request.ProductVariantId &&
                                          p.Product.Store.SellerId == seller.Id);
            if (productVariant == null)
                return NotFound<string>(localize.Get("ProductNotFound"));




            // 3️⃣ Upload image
            var imagePath = await fileService.UploadGenericAsync(
            UploadFolder.Store,
            request.ProductVariantId,
            request.Image);

            if (!imagePath.StartsWith("/"))
            {
                return BadRequest<string>(localize.Get(imagePath));
            }

           


            // 5️⃣ Save in DB
            var productVariantImage = new ProductVariantImage
            {
                ProductVariantId = request.ProductVariantId,
                MediaPath = imagePath,

            };

            await productVariantImageService.AddAsync(productVariantImage);
            await productVariantImageService.SaveChangesAsync();

            return Success(localize.Get("ImageAddedSuccessfully"));


        }




        //public async Task<Response<string>> Handle(UpdateProductVariantImageCommand request, CancellationToken cancellationToken)
        //{
        //    var currentUserId = currentUserService.GetUserId();

        //    // 1️⃣ Get image
        //    var image = await productVariantImageService.GetTableAsTracking()
        //                .Include(i => i.ProductVariant)
        //                .ThenInclude(v => v.Product)
        //                .ThenInclude(p => p.Store)
        //                .FirstOrDefaultAsync(i => i.Id == request.ImageId &&
        //                 i.ProductVariant.Product.Store.SellerId == currentUserId);


        //    if (image == null)
        //        return NotFound<string>(localize.Get("ImageNotFound"));




        //    if (request.IsMain && !image.IsMain)
        //    {
        //        var oldMainImages = await productVariantImageService.GetTableAsTracking()
        //            .Where(i => i.ProductVariantId == image.ProductVariantId && i.IsMain)
        //           .ExecuteUpdateAsync(setters =>
        //                setters.SetProperty(i => i.IsMain, false));
        //    }

        //    // 4️⃣ Update fields
        //    image.IsMain = request.IsMain;
        //    image.SortOrder = request.SortOrder;

        //    await productVariantImageService.UpdateAsync(image);
        //    await productVariantImageService.SaveChangesAsync();

        //    return Success<string>(localize.Get("ImageUpdatedSuccessfully"));
        //}

        //public async Task<Response<string>> Handle(DeleteProductVariantImageCommand request, CancellationToken cancellationToken)
        //{
        //    // 1️⃣ Get image with related product
        //    var currentUserId = currentUserService.GetUserId();

        //    var image = await productVariantImageService.GetTableAsTracking()
        //                .Include(i => i.ProductVariant)
        //                .ThenInclude(v => v.Product)
        //                .ThenInclude(p => p.Store)
        //                .FirstOrDefaultAsync(i => i.Id == request.ImageId &&
        //                 i.ProductVariant.Product.Store.SellerId == currentUserId);
        //    if (image == null)
        //        return NotFound<string>(localize.Get("ImageNotFound"));

        //    if (image.IsMain)
        //    {
        //        var anotherImage = await productVariantImageService.GetTableAsTracking()
        //            .FirstOrDefaultAsync(i => i.ProductVariantId == image.ProductVariantId && i.Id != image.Id);

        //        if (anotherImage != null)
        //            anotherImage.IsMain = true;
        //    }

        //    // 3️⃣ Delete image
        //    if (!string.IsNullOrWhiteSpace(image.MediaPath))
        //    {
        //        await fileService.DeleteFile(image.MediaPath);
        //    }
        //    await productVariantImageService.DeleteAsync(image);
        //    await productVariantImageService.SaveChangesAsync();

        //    return Success<string>(localize.Get("ImageDeletedSuccessfully"));
        //}





        #endregion
    }
}


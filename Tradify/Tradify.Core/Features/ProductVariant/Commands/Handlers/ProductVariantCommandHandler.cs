using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.ProductVariant.Commands.Models;
using Tradify.Core.Features.ProductVariant.Commands.Models;
using Tradify.Core.Features.Store.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Infrastructure.Context;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;
using System.Text.Json;


namespace Tradify.Core.Features.ProductVariant.Commands.Handlers
{
    public class ProductVariantCommandHandler : ResponseHandler,
                                        IRequestHandler<AddProductVariantCommand, Response<string>>,
                                        IRequestHandler<AddProductVarintWithImageCommand, Response<string>>
       
                                        ,IRequestHandler<UpdateProductVariantCommand, Response<string>>,
                                         IRequestHandler<DeleteProductVariantCommand, Response<string>>
                                        , IRequestHandler<AddDiscountCommand, Response<string>>
                                        , IRequestHandler<DeleteDiscountCommand, Response<string>>
                                        , IRequestHandler<RestoreProductVariantCommand, Response<string>>


    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly IProductVariantService productVariantService;
        private readonly IStoreService storeService;
        private readonly IMapper mapper;
        private readonly IProductService productService;
        private readonly ICurrentUserService currentUserService;
        private readonly IFileService fileService; 
        private readonly IProductVariantImageService productVariantImageService;


        #endregion

        #region Constructor
        public ProductVariantCommandHandler(IProductVariantService productVariantService,
                                     IMapper mapper,
                                     IProductService productService,
                                     IStoreService storeService,
                                     ICurrentUserService currentUserService,
                                     LocalizationService localize,
                                     IFileService fileService,
                                     IProductVariantImageService productVariantImageService) : base(localize)
        {
            this.productVariantService = productVariantService;
            this.mapper = mapper;
            this.localize = localize;
            this.storeService = storeService;
            this.productService = productService;
            this.productVariantImageService = productVariantImageService; 
            this.currentUserService = currentUserService;
            this.fileService = fileService; 
        }
        #endregion

        #region Methods
        // Add Product Variant 


        public async Task<Response<string>> Handle(AddProductVariantCommand request, CancellationToken cancellationToken)
        {
            var variants = mapper.Map<ProductVariants>(request);


            var result = await productVariantService.AddProductVariantAsync(variants);//,request.StoreId);

            if (result.Item1 != "Success")
            {
                return BadRequest<string>(localize.Get(result.Item1));
            }
            else
            {
                return Success<string>("Success", meta: result.Item2);
            }

        }



        //Add ProductVarint With Image


        public async Task<Response<string>> Handle(AddProductVarintWithImageCommand request, CancellationToken cancellationToken)
        {
            var variants = mapper.Map<ProductVariants>(request);


            var result = await productVariantService.AddProductVariantAsync(variants);//,request.StoreId);

            if (result.Item1 != "Success")
            {
                return BadRequest<string>(localize.Get(result.Item1));
            }


            var varintId = result.Item2.Value;

            // ✅ رفع الصورة
            var imagePath = await fileService.UploadGenericAsync(
                UploadFolder.Variants,
                varintId,
                request.Image);

            if (!imagePath.StartsWith("/"))
            {

                return BadRequest<string>(localize.Get(imagePath));
            }

            // ✅ حفظ الصورة
            var varintImage = new Data.Entities.ProductVariantImage
            {
                ProductVariantId = varintId,
                MediaPath = imagePath
            };

            await productVariantImageService.AddAsync(varintImage);
            await productVariantImageService.SaveChangesAsync();

            return Success<string>("Success", meta: varintId);

        }


















        public async Task<Response<string>> Handle(UpdateProductVariantCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = currentUserService.GetUserId();

            var store = await storeService.GetBySellerIdAsync(currentUserId);

            if (store == null)
                return NotFound<string>(localize.Get("StoreNotFound"));

            var variant = await productVariantService
                .GetTableNoTracking()
                .FirstOrDefaultAsync(v =>
                    v.Id == request.Id &&
                    v.Product.StoreId == store.Id,
                    cancellationToken);

            if (variant == null)
                return NotFound<string>(localize.Get("VariantNotFound"));
            

           
            // تحقق من التكرار (Color + Size)
            var exists = await productVariantService.GetTableNoTracking()
                .AnyAsync(v =>
                    v.ProductId == variant.ProductId &&
                    v.Id != variant.Id &&
                    v.Color == request.Color &&
                    v.Size == request.Size,
                    cancellationToken);

            if (exists)
                return BadRequest<string>(localize.Get("VariantAlreadyExists"));

            // تحديث البيانات
            mapper.Map(request, variant);

            await productVariantService.UpdateAsync(variant);
            await productVariantService.SaveChangesAsync();

            return Success<string>(localize.Get("VariantupdatedSuccessfully"));
        }


        public async Task<Response<string>> Handle(DeleteProductVariantCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = currentUserService.GetUserId();

            var store = await storeService.GetBySellerIdAsync(currentUserId);

            if (store == null)
                return NotFound<string>(localize.Get("StoreNotFound"));

            var variant = await productVariantService
                .GetTableAsTracking()
                .Include(v => v.Product)
                .FirstOrDefaultAsync(v =>
                    v.Id == request.Id &&
                    v.Product.StoreId == store.Id,
                    cancellationToken);

            if (variant == null)
                return NotFound<string>(localize.Get("VariantNotFound"));

            if (variant.Product == null || variant.Product.IsDeleted)
                return BadRequest<string>(localize.Get("CannotRestoreVariantProductDeleted"));

            if (variant.IsDeleted)
                return BadRequest<string>(localize.Get("VariantAlreadyDeleted"));
            // Soft Delete
            variant.IsDeleted = true;
            await productVariantService.UpdateAsync(variant);

            await productVariantService.SaveChangesAsync();

            return Success<string>(localize.Get("VariantDeletedSuccessfully"));
        }
        public async Task<Response<string>> Handle(RestoreProductVariantCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = currentUserService.GetUserId();

            var store = await storeService.GetBySellerIdAsync(currentUserId);
            if (store == null)
                return NotFound<string>(localize.Get("StoreNotFound"));

            var variant = await productVariantService
                .GetTableAsTracking().IgnoreQueryFilters()
                .Include(v => v.Product)
                .FirstOrDefaultAsync(v =>
                    v.Id == request.Id &&
                    v.Product.StoreId == store.Id,
                    cancellationToken);

            if (variant == null)
                return NotFound<string>(localize.Get("VariantNotFound"));

            // ✅ تحقق من حالة المنتج نفسه
            if (variant.Product == null || variant.Product.IsDeleted)
                return BadRequest<string>(localize.Get("CannotRestoreVariantProductDeleted"));

            if (!variant.IsDeleted)
                return BadRequest<string>(localize.Get("VariantIsNotDeleted"));

            // Restore Variant
            variant.IsDeleted = false;
            await productVariantService.UpdateAsync(variant);
            await productVariantService.SaveChangesAsync();

            return Success<string>(localize.Get("VariantRestoredSuccessfully"));
        }
       

        public async Task<Response<string>> Handle(AddDiscountCommand request, CancellationToken cancellationToken)
        {
            var variant = await productVariantService.GetByIdAsync(request.VariantId);

            if (variant == null)
                NotFound<string>(localize.Get("ProductVariantNotFound"));
            
                variant.Discount = request.Discount;

                 await productVariantService.UpdateAsync(variant);

            return Success<string>(localize.Get("DiscountAddedSuccessfully"));
            

        }
        public async Task<Response<string>> Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
        {
            var variant = await productVariantService.GetByIdAsync(request.VariantId);

            if (variant == null)
                return NotFound<string>(localize.Get("ProductVariantNotFound"));

            variant.Discount = 0;

            await productVariantService.UpdateAsync(variant);

            return Success<string>(localize.Get("DiscountDeletedSuccessfully"));
        }

        #endregion
    }
}


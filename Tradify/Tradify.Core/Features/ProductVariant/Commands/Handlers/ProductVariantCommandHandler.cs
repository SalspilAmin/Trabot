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
                                         IRequestHandler<AddProductVariantCommand, Response<string>>
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


        #endregion

        #region Constructor
        public ProductVariantCommandHandler(IProductVariantService productVariantService,
                                     IMapper mapper,
                                     IProductService productService,
                                     IStoreService storeService,
                                     ICurrentUserService currentUserService,
                                     LocalizationService localize) : base(localize)
        {
            this.productVariantService = productVariantService;
            this.mapper = mapper;
            this.localize = localize;
            this.storeService = storeService;
            this.productService = productService;
          
            this.currentUserService = currentUserService;
        }
        #endregion

        #region Methods

        public async Task<Response<string>> Handle(AddProductVariantCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = currentUserService.GetUserId();

            var store = await storeService.GetBySellerIdAsync(currentUserId);

            if (store == null)
                return NotFound<string>(localize.Get("StoreNotFound"));
            
            var product = await productService
                .GetTableNoTracking()
                .FirstOrDefaultAsync(p =>
                    p.Id == request.ProductId &&
                    p.StoreId == store.Id,
                    cancellationToken);
        
            if (product == null )
                return NotFound<string>(localize.Get("ProductNotFound"));


            // 4️⃣ Check Duplicate Variant (ProductId + Color + Size)
            var exists = await productVariantService
                .GetTableNoTracking()
                .AnyAsync(v =>
                    v.ProductId == request.ProductId &&
                    v.Color == request.Color &&
                    v.Size == request.Size ,
                    cancellationToken);

            if (exists)
            return BadRequest<string>(localize.Get("VariantAlreadyExists"));

            var variant = mapper.Map<ProductVariants>(request);
            variant.ProductId = request.ProductId;
          
            await productVariantService.AddAsync(variant);
            await productVariantService.SaveChangesAsync();
            
            return Success<string>( localize.Get("VariantAddedSuccessfully"));
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


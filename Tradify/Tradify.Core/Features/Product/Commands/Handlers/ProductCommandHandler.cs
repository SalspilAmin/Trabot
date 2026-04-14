using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.ProductVariant.Commands.Models;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.AuthorizationServices;
using Tradify.Service.Services;
using Tradify.Service.Services.AuthorizationServices;

namespace Tradify.Core.Features.Product.Commands.Handlers
{
    public class ProductCommandHandler : ResponseHandler,
                                         IRequestHandler<AddProductCommand, Response<string>> ,
                                         IRequestHandler<UpdateProductCommand, Response<string>>,
                                         IRequestHandler<DeleteProductCommand, Response<string>>,
                                         IRequestHandler<RestoreProductCommand, Response<string>>




    {
        #region Fields
        private readonly LocalizationService localize;
        private readonly IProductService  productService;
        private readonly IStoreService storeService;
        private readonly IProductVariantService productVariantService;
        private readonly IMapper mapper;
        private readonly ICurrentUserService currentUserService;
        private readonly ICateroriesService cateroriesService;
        
        private readonly IAuthorizationService  authorizationService;

        #endregion

        #region Constructor
        public ProductCommandHandler(IProductService productService,
                                     IMapper mapper,
                                     IStoreService storeService,
                                     ICateroriesService cateroriesService,
                                     ICurrentUserService currentUserService,
                                     IAuthorizationService authorizationService,
                                     LocalizationService localize) : base(localize)
        {
            this.productService = productService;
            this.mapper = mapper;
            this.localize = localize;
            this.storeService = storeService;  
            this.cateroriesService = cateroriesService;
            this.currentUserService = currentUserService;
            this.authorizationService = authorizationService;   
        }
        #endregion

        #region Methods

        // Add Product

        public async Task<Response<string>> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();
            // Check Store
            var store = await storeService.GetBySellerIdAsync(userId);

            if (store == null)
                return NotFound<string>(localize.Get("YouMustCreateStoreFirst"));
            
            // Check Category
            var category = await cateroriesService.GetByIdAsync(request.CategoryId);

            if (category == null)
                return BadRequest<string>(localize.Get("CategoryNotFound"));
            // Mapping
            var product = mapper.Map<Products>(request);
            // Assign Store
            product.StoreId = store.Id;
            product.UpdateAt = DateTime.UtcNow;
            product.CreatedAt = DateTime.UtcNow;

            using var transaction = await productService.BeginTransactionAsync();

            try
            {
                await productService.AddAsync(product);
                await productService.SaveChangesAsync();

                var variant = new ProductVariants
                {
                    ProductId = product.Id,
                    Price = request.Price,
                    NumberOfProductInStock = request.Stock,
                    Color = null,
                    Size = null,
                    Discount = 0,
                    MetaData = null
                };

                await productVariantService.AddAsync(variant);
                await productVariantService.SaveChangesAsync();

                await transaction.CommitAsync();

                return Success(localize.Get("ProductAddedSuccessfully"));
            }
            catch
            {
                await transaction.RollbackAsync();
                return BadRequest<string>(localize.Get("ErrorWhileAddingProduct"));
            }
            //// Save Product
            //await productService.AddAsync(product);
            //await productService.SaveChangesAsync();
            //// Assign Productvariant

            //var variant = new ProductVariants
            //{
            //    ProductId = product.Id,
            //    Price = request.Price,
            //    NumberOfProductInStock = request.Stock,
            //    Color = null,
            //    Size = null,
            //    Discount = 0,
            //    MetaData = null
            //};
            //// Save Productvariant

            //await productVariantService.AddAsync(variant);
            //await productVariantService.SaveChangesAsync();

            //return Success<string>(localize.Get("ProductAddedSuccessfully"));

        }
        


        //// Update Product
       
        public async Task<Response<string>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = currentUserService.GetUserId();

            var product = await productService
                      .GetTableAsTracking()
                     .Where(p => p.Id == request.ProductId && p.Store.SellerId == currentUserId)
                     .FirstOrDefaultAsync();

            if (product == null)
                return NotFound<string>(localize.Get("ProductNotFound"));
            //var product = await productService.GetByIdAsync(request.ProductId);

            //if (product == null)
            //    return NotFound<string>(localize.Get("ProductNotFound"));

            // التأكد أن هذا المنتج تابع للسيلر الحالي
            //var sellerId = currentUserService.GetUserId();
            //if (product.Store?.SellerId != currentUserId)
            //    return Unauthorized<string>(localize.Get("YouCannotUpdateThisProduct"));

            // التأكد من وجود الكاتيجوري
            var category = await cateroriesService.GetByIdAsync(request.CategoryId);

            if (category == null)
                return BadRequest<string>(localize.Get("CategoryNotFound"));
            // Mapping
            mapper.Map(request, product);

            product.UpdateAt = DateTime.UtcNow;

            await productService.UpdateAsync(product);
            await productService.SaveChangesAsync();

            return Success<string>(localize.Get("ProductUpdatedSuccessfully"));

           
        }
        //// Remove Product

        
        public async Task<Response<string>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();
            var isAdmin = await authorizationService.IsUserInRoleAsync(userId, "Admin");
            var product = await productService
                   .GetTableAsTracking()
                   .Include(p => p.ProductVariants)
                   .Include(p => p.Store)
                   .FirstOrDefaultAsync(p => p.Id == request.Id && (p.Store.SellerId == userId || isAdmin));

            if (product == null)
                return NotFound<string>(localize.Get("ProductNotFound"));

          
            //// التأكد أن هذا المنتج تابع للسيلر الحالي

            //if (product.Store?.SellerId != userId && !isAdmin)
            //    return Unauthorized<string>(localize.Get("YouCannotDeletThisProduct"));


            using var transaction = await productService.BeginTransactionAsync();

            try
            {
                // Soft Delete المنتج
                product.IsDeleted = true;
                product.UpdateAt = DateTime.UtcNow;

                // Soft Delete لكل الـ ProductVariants
                if (product.ProductVariants != null)
                {
                    foreach (var variant in product.ProductVariants)
                    {
                        variant.IsDeleted = true;
                    }
                }
                await productService.UpdateAsync(product);
                await productService.SaveChangesAsync();

                // Commit Transaction
                await transaction.CommitAsync();

                return Success<string>(localize.Get("ProductDeletedSuccessfully"));
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest<string>(localize.Get("ProductDeleteFailed"));
            }
            //if (product.ProductVariants != null)
            //{
            //    foreach (var variant in product.ProductVariants)
            //    {
            //        variant.IsDeleted = true;
            //    }
            //    await productVariantService.SaveChangesAsync();

            //}

           
        }


        public async Task<Response<string>> Handle(RestoreProductCommand request, CancellationToken cancellationToken)
        {
            var currentUserId = currentUserService.GetUserId();

            var store = await storeService.GetBySellerIdAsync(currentUserId);

            if (store == null)
                return NotFound<string>(localize.Get("StoreNotFound"));

           
            var product = await productService
                                   .GetTableAsTracking()
                                   .IgnoreQueryFilters()
                                   .Where(p => p.Id == request.Id && p.StoreId == store.Id)
                                   .FirstOrDefaultAsync(cancellationToken);

            if (product == null)
                return NotFound<string>(localize.Get("ProductNotFound"));

            if (!product.IsDeleted)
                return BadRequest<string>(localize.Get("ProductIsAlreadyActive"));

            product.IsDeleted = false;
            if (product.ProductVariants != null)
            {
                foreach (var variant in product.ProductVariants)
                {
                    variant.IsDeleted = false;
                }
            }
            product.UpdateAt = DateTime.UtcNow;


            await productService.UpdateAsync(product);
            await productService.SaveChangesAsync();

            return Success(localize.Get("ProductRestoredSuccessfully"));
        }




        //public async Task<Response<string>> Handle(AddDiscountCommand request, CancellationToken cancellationToken)
        //{
        //    var product = await productService.GetTableAsTracking().Include(p=>p.ProductVariants)
        //        .FirstOrDefaultAsync(p=>p.Id==request.Id);

        //    if (product == null)
        //        return NotFound<string>(localize.Get("ProducttNotFound"));

        //    if (product.ProductVariants == null || !product.ProductVariants.Any())
        //        return NotFound<string>(localize.Get("NoVariantsFound"));

        //    foreach (var variant in product.ProductVariants)
        //    {
        //        variant.Discount = request.Discount;

        //    }



        //    await productService.SaveChangesAsync();

        //    return Success<string>(localize.Get("DiscountAddedSuccessfully"));


        //}
        #endregion
    }
}

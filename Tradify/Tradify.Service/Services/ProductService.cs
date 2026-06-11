using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Crypto;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Enums;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;
using Twilio.TwiML.Voice;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Service.Services
{
    public class ProductService : Service<Products>, IProductService
    {
        //public ProductService(IGenericRepository<Products> repository) : base(repository)
        //{
        //}
        private readonly IProductRepository productRepository;
        private readonly ApplicationDbContext context;
        private readonly IStoreService storeService;
        private readonly ICurrentUserService currentUserService;
        private readonly ICateroriesService cateroriesService;
        private readonly ISellerService sellerService;
        private readonly ILogger<ProductService> logger;

        public ProductService(IProductRepository repository, ApplicationDbContext context, IStoreService storeService,
                                     ICateroriesService cateroriesService,
                                     ICurrentUserService currentUserService, ISellerService sellerService
            , ILogger<ProductService> logger) : base(repository)
        {
            productRepository = repository;
            this.context = context;
            this.storeService = storeService;
            this.cateroriesService = cateroriesService;
            this.currentUserService = currentUserService;
            this.sellerService = sellerService;
            this.logger = logger;
        }

        public async Task<Products> GetByIdWithIncludesAsync(int id)
        {
            try
            {
                return await productRepository.GetByIdWithIncludesAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public IQueryable<Products> GetProductsByCategoryAsync(int categoryId)
        {
            try
            {
                return productRepository.GetProductsByCategoryAsync(categoryId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<(string, int?, int?)> AddProductWithDefultVarintAsync(Products product , ProductVariants variant)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    //  1. Check if seller exist
                    var ValidSeller = await currentUserService.GetValidSellerContextAsync();

                    if (ValidSeller.Error != null)
                        return (ValidSeller.Error, null, null);

                    // 2. Get Seller , Store
                    var seller = ValidSeller.Seller;
                    var store = ValidSeller.Store;

                    //3. Cheack If Store Type Is Service 

                    if (store.Type != StoreType.Product)

                        return ("ThisActionAllowedForProductStoresOnly", null, null);



                    //5. Check Category
                    var category = await cateroriesService.GetByIdAsync(product.CategoryId);

                    if (category == null)
                        return ("CategoryNotFound", null, null);



                    //5. check Duplicate product Name 

                    var productNameExists = await context.Products.AnyAsync(s => s.Name == product.Name);
                    if (productNameExists)
                        return ("ProductNameAlreadyExists", null, null);



                    //  1. Default values
                    product.StoreId = store.Id;
                    product.UpdateAt = DateTime.UtcNow;
                    product.CreatedAt = DateTime.UtcNow;

                    // 2. Save
                    await context.Products.AddAsync(product);
                    await context.SaveChangesAsync();

                    var exists = await context.ProductVariants.AnyAsync(v => v.ProductId == product.Id &&
                                                                             v.Color == null &&
                                                                             v.Size == null);

                    if (exists)
                        return ("VariantAlreadyExists", null, null);

                    // 3. Create Default Variant 
                    variant.ProductId = product.Id;
                    variant.Color = null;
                    variant.Size = null;
                    variant.Discount = 0;
                    variant.Name = product.Name;

                    
                   // await context.Products.AddAsync(product);
                    await context.ProductVariants.AddAsync(variant);

                    await context.SaveChangesAsync();

                    await transaction.CommitAsync();
                    return ("Success", product.Id, variant.Id);
                }

                catch (Exception ex)

                {

                    await transaction.RollbackAsync();
                    logger.LogError(ex, ex.Message);
                    throw;
                }
            }

        }
    }
}


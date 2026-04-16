using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;
using Microsoft.EntityFrameworkCore;


namespace Tradify.Service.Services
{
    public class ProductVariantService : Service<ProductVariants>, IProductVariantService
    {
        //public ProductVariantService(IGenericRepository<ProductVariants> repository  ) : base(repository)
        //{
        //}
        private readonly IProductVariantRepository repository;
        private readonly ApplicationDbContext context;
        private readonly IStoreService storeService;
        private readonly ICurrentUserService currentUserService;
        private readonly ICateroriesService cateroriesService;
        private readonly ISellerService sellerService;
        private readonly IProductService productService;    

        public ProductVariantService(IProductVariantRepository repository, ApplicationDbContext context, IStoreService storeService,
                                     ICateroriesService cateroriesService,
                                     ICurrentUserService currentUserService, ISellerService sellerService, IProductService productService) : base(repository)
        {
           this. repository = repository;
            this.context = context;
            this.storeService = storeService;
            this.cateroriesService = cateroriesService;
            this.currentUserService = currentUserService;
            this.sellerService = sellerService;
            this.productService = productService;   
        }



        public async Task<(string, int?)> AddProductVariantAsync(ProductVariants variants) //,int storeid)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    //  1. Check if seller exist

                    var userId = currentUserService.GetUserId();
                    var seller = await sellerService.GetTableNoTracking().FirstOrDefaultAsync(x => x.UserId == userId);

                    if (seller == null)
                        return ("SellerNotFound", null);

                    //2. Check if seller active
                    if (seller.IsActive == false)
                        return ("SellerNotActive", null);

                    //3.cheack if seller conected with user deleated 
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                    if (user == null)
                        return ("UserNotFound", null);
                    if (user.IsDeleted)
                        return ("SellerConectWithDeletedUser", null);

                    //4.check  Store 

                    var store = await storeService.GetBySellerIdAsync(seller.Id);

                    //var store = await context.Stores
                    //             .FirstOrDefaultAsync(x => x.Id == storeid);
                    if (store == null)
                        return ("YouMustCreateStoreFirst", null);



                    //5. Check Product
                    var product = await productService.GetByIdAsync(variants.ProductId);

                    if (product == null)
                        return ("ProductNotFound", null);



                    //5. Check Duplicate Variant (ProductId + Color + Size)


                    var exists = await  GetTableNoTracking()
                        .AnyAsync(v =>
                            v.ProductId == variants.ProductId &&
                            v.Color == variants.Color &&
                            v.Size == variants.Size);

                    if (exists)
                        return ("VariantAlreadyExists", null);



                    //  1. Default values
                    variants.ProductId = product.Id;
                    variants.Name = product.Name +" "+ variants.Color+" " + variants.Size;

                    // 2. Save
                    await AddAsync(variants);
                    await SaveChangesAsync();

                    await transaction.CommitAsync();
                    return ("Success", variants.Id);
                }

                catch (Exception ex)

                {

                    await transaction.RollbackAsync();
                    return ("Failed", null);

                }
            }

        }



    }
}

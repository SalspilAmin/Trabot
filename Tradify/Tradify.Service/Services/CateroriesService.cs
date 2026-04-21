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
    public class CateroriesService : Service<Categories>, ICateroriesService
    {
        private readonly ApplicationDbContext context;
        private readonly ISellerService sellerService;
        private readonly ICurrentUserService currentUserService;
        private readonly ICategoryRepository repository;
        private readonly IStoreService storeService;

        public CateroriesService(ICategoryRepository repository, ApplicationDbContext context,
            ISellerService sellerService, ICurrentUserService currentUserService,
            IStoreService storeService) : base(repository)
        {
            this.repository = repository;
            this.sellerService = sellerService;
            this.currentUserService = currentUserService;
            this.context = context;
            this.storeService = storeService;

        }



        public async Task<(string, int?)> AddCategoriesAsync(Categories categories) //, int sellerId)
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    //  1. Check if seller exist

                    var userId = currentUserService.GetUserId();
                    var seller = await sellerService.GetTableNoTracking().FirstOrDefaultAsync(x => x.UserId == userId);

                    //var seller = await sellerService.GetTableNoTracking().FirstOrDefaultAsync(x => x.Id == sellerId);

                    if (seller == null)
                        return ("SellerNotFound", null);

                    //2. Check if seller active
                    if (seller.IsActive == false)
                        return ("SellerNotActive", null);

                    //// 3.cheack if seller conected with user deleated
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                    if (user == null)
                        return ("UserNotFound", null);
                    if (user.IsDeleted)
                        return ("SellerConectWithDeletedUser", null);

                    //4.check  Store 

                    var store = await storeService.GetBySellerIdAsync(seller.Id);
                  

                    if (store == null)
                        return ("YouMustCreateStoreFirst", null);

                    if (store.IsDeleted) return ("YouCantUpdateInStoreDeleted",null);

                    //Ckeack category parant

                    if (categories.ParentCategoryId.HasValue)
                    {
                        var parent = await GetTableAsTracking()
                            .FirstOrDefaultAsync(c => c.Id == categories.ParentCategoryId.Value);
                        if (parent == null)
                            return ("ParentCategoryNotFound",null); 
                    }


                    //5. check Duplicate Category Name 

                    var categoryNameExists = await context.Categories.AnyAsync(s => s.Name == categories.Name 
                    && s.ParentCategoryId == categories.ParentCategoryId);
                    if (categoryNameExists)
                        return ("CategoryNameAlreadyExists", null);



                    //  1. Default values
                    categories.StoreId = store.Id;
                    categories.IsDeleted = false;

                    // 2. Save
                    await AddAsync(categories);
                    await SaveChangesAsync();



                    await transaction.CommitAsync();
                    return ("Success", categories.Id);
                }

                catch (Exception ex)

                {

                    await transaction.RollbackAsync();
                    return ("Failed", null);

                }
            }

        }

        public async Task<string> UpdateCategory(Categories categories)//, int sellerId) 
        {
            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    //  1. Check if seller exist

                    var userId = currentUserService.GetUserId();
                    var seller = await sellerService.GetTableNoTracking().FirstOrDefaultAsync(x => x.UserId == userId);

                    //var seller = await sellerService.GetTableNoTracking().FirstOrDefaultAsync(x => x.Id == sellerId);

                    if (seller == null)
                        return ("SellerNotFound");

                    //2. Check if seller active
                    if (seller.IsActive == false)
                        return ("SellerNotActive");

                    //3.cheack if seller conected with user deleated 
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);
                    if (user == null)
                        return ("UserNotFound");
                    if (user.IsDeleted)
                        return ("SellerConectWithDeletedUser");

                    //4.check  Store 

                    var store = await storeService.GetBySellerIdAsync(seller.Id);


                    if (store == null)
                        return ("YouMustCreateStoreFirst");

                    if (store.IsDeleted) return ("YouCantUpdateInStoreDeleted");

                    //  Get Category 

                    var category = await GetTableAsTracking().FirstOrDefaultAsync(
                        c=>c.Id==categories.Id && c.StoreId==store.Id
                        );
                    if (category == null)
                        return ("CategoryNotFound");


                    //Ckeack category parant

                    if (categories.ParentCategoryId.HasValue)
                    {
                        if (categories.ParentCategoryId == categories.Id)
                            return "CategoryCannotBeParentOfItself";

                        var parent = await GetTableAsTracking()
                            .FirstOrDefaultAsync(c => c.Id == categories.ParentCategoryId.Value);
                        if (parent == null)
                            return ("ParentCategoryNotFound");

                        // Circular check
                        if (await IsCircularAsync(categories.Id, categories.ParentCategoryId.Value))
                            return ("InvalidParentCircularReference");

                        if (parent.StoreId != store.Id)
                            return "InvalidParentForThisStore";
                    }

                    // Duplicate check
                    var exists = await GetTableNoTracking()
                        .AnyAsync(c =>
                            c.Id != categories.Id &&
                            EF.Functions.Like(c.Name, categories.Name) &&
                            c.ParentCategoryId == categories.ParentCategoryId);

                    if (exists)
                        return "CategoryNameAlreadyExists";

                    //  1. Default values
                    category.StoreId = store.Id;
                    category.IsDeleted = false;
                    category.ParentCategoryId = categories.Id;
                    category.Name=categories.Name;


                    // 2. Save
                    await SaveChangesAsync();



                    await transaction.CommitAsync();
                    return ("Success");
                }

                catch (Exception ex)

                {

                    await transaction.RollbackAsync();
                    return ("Failed");

                }
            }


        }



        private async Task<bool> IsCircularAsync(int categoryId, int newParentId)
        {
            var current = newParentId;

            while (true)
            {
                var parent = await GetTableNoTracking()
                    .Where(c => c.Id == current)
                    .Select(c => c.ParentCategoryId)
                    .FirstOrDefaultAsync();

                if (parent == null)
                    return false;

                if (parent == categoryId)
                    return true;

                current = parent.Value;
            }
        }





    }
}

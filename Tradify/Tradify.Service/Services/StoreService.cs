using Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Infrastructure.Repositories;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public class StoreService : Service<Stores>, IStoreService
    {
        //public StoreService(IGenericRepository<Stores> repository) : base(repository)
        //{
        //}

        private readonly IStoreRepository storeRepository;
        private readonly ApplicationDbContext context;

        public StoreService(IStoreRepository repository, ApplicationDbContext context)
            : base(repository)
        {
            this.context = context;
            storeRepository = repository;
        }
        public async Task<Stores> GetByIdWithIncludesAsync(int id)
        {
            try
            {
                return await storeRepository.GetByIdWithIncludesAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<Stores?> GetTableIgnoreQueryFilters()
        {
            try
            {
                return  storeRepository.GetTableIgnoreQueryFilters();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Stores?> GetBySellerIdAsync(int sellerId)
        {
            try
            {
                return await storeRepository.GetBySellerIdAsync(sellerId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<(string, int?)> AddStoreAsync(Stores stores)
        {
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    //  1. Check if seller exist

                    var seller = await context.Sellers.FirstOrDefaultAsync(s => s.Id == stores.SellerId);

                    if (seller == null) 
                        return ( "SellerNotFound" ,null);

                    //2. Check if seller active
                    if(seller.IsActive==false)
                        return ("SellerNotActive",null);

                    //3.cheack if seller conected with user deleated 
                    var user = await context.Users.FirstOrDefaultAsync(u => u.Id == seller.UserId);


                    if (user == null)
                        return ("UserNotFound", null);

                    if (user.IsDeleted )
                        return ("SellerConectWithDeletedUser", null);

                    //4.check if seller have store 

                    var existingStore = await context.Stores.AnyAsync(s => s.SellerId == stores.SellerId);

                    if (existingStore )
                           return ("SellerAlreadyHasStore", null);

                    //5. check Duplicate Store Name 

                    var storeNameExists = await context.Stores.AnyAsync(s => s.Name == stores.Name);
                    if (storeNameExists)
                        return ("StoreNameAlreadyExists", null);

                    

                    //  1. Default values
                    stores.IsActive = true;
                    stores.Name = stores.Name.Trim().ToLower();

                    // 2. Save
                    await AddAsync(stores);
                    await SaveChangesAsync();

                    await transaction.CommitAsync();
                    return ("Success", stores.Id);
                }

                catch (Exception ex)

                {

                    await transaction.RollbackAsync();
                    return ("Failed", null) ;

                }
            }
        }


    }
}

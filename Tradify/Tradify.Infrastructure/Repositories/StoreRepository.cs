using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Infrastructure.Repositories
{
    public class StoreRepository : GenericRepository<Stores>, IStoreRepository
    {
        #region Filds
        private DbSet<Stores> stores;
        #endregion

        #region Constructor
        public StoreRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            stores = applicationDbContext.Set<Stores>();
        }

        #endregion

        #region Methods
        public async Task<Stores?> GetByIdWithIncludesAsync(int id)
        {
            return await stores
                .Include(x => x.StoreImage)
                 .Include(s => s.Seller)
                 .ThenInclude(x => x.User)
                 .FirstOrDefaultAsync(p => p.Id == id);
        }

        public IQueryable<Stores?> GetTableIgnoreQueryFilters()
        {
            return stores.IgnoreQueryFilters();
        }

        public async Task<Stores?> GetBySellerIdAsync(int sellerId)
        {
            return await stores
                
                .FirstOrDefaultAsync(s => s.SellerId == sellerId);
        }
        #endregion

    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;

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

        #endregion

    }
}

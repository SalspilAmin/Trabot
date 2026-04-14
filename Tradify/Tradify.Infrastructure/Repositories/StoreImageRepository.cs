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
    public class StoreImageRepository : GenericRepository<StoreImage>, IStoreImageRepository
    {
        #region Filds
        private DbSet<StoreImage> storeImage;
        #endregion

        #region Constructor
        public StoreImageRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            storeImage = applicationDbContext.Set<StoreImage>();
        }

        #endregion

        #region Methods

        #endregion

    }
}

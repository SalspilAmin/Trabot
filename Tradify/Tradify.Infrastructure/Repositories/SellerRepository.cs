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
    public class SellerRepository : GenericRepository<Sellers>, ISellerRepository
    {
        #region Filds
        private DbSet<Sellers> sellers;
        #endregion

        #region Constructor
        public SellerRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            sellers = applicationDbContext.Set<Sellers>();
        }

        #endregion

        #region Methods

        #endregion
    }
}

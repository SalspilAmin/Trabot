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
    public class SubOrderRepository : GenericRepository<SubOrders>, ISubOrderRepository
    {
        #region Filds
        private DbSet<SubOrders> subOrders;
        #endregion

        #region Constructor
        public SubOrderRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            subOrders = applicationDbContext.Set<SubOrders>();
        }

        #endregion

        #region Methods

        #endregion

    }
}

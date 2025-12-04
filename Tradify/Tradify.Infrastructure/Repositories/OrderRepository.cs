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
    public class OrderRepository : GenericRepository<Orders>, IOrderRepository
    {
        #region Filds
        private DbSet<Orders> orders;
        #endregion

        #region Constructor
        public OrderRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            orders = applicationDbContext.Set<Orders>();
        }

        #endregion

        #region Methods

        #endregion
    }
}

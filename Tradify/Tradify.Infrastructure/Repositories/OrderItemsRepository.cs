using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Entities.Chat;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;

namespace Tradify.Infrastructure.Repositories
{
    public class OrderItemsRepository : GenericRepository<OrderItems>, IOrderItemsRepository
    {
        #region Filds
        private DbSet<OrderItems> orderItems;
        #endregion

        #region Constructor
        public OrderItemsRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            orderItems = applicationDbContext.Set<OrderItems>();
        }

        #endregion

        #region Methods

        #endregion
    }
}

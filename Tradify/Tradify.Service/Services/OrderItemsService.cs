using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public class OrderItemsService : Service<OrderItems>, IOrderItemsService
    {
        public OrderItemsService(IGenericRepository<OrderItems> repository) : base(repository)
        {
        }
    }
}

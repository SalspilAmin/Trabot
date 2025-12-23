using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public class OrdersService : Service<Orders>, IOrdersService
    {
        public OrdersService(IGenericRepository<Orders> repository) : base(repository)
        {
        }
    }
}

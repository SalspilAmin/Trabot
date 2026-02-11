using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public class CartProductService : Service<CartProduct>, ICartProductService
    {
        public CartProductService(IGenericRepository<CartProduct> repository) : base(repository)
        {
        }
    }
}

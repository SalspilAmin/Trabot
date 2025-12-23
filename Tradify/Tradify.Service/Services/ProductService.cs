using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public class ProductService : Service<Products>, IProductService
    {
        public ProductService(IGenericRepository<Products> repository) : base(repository)
        {
        }
    }
}

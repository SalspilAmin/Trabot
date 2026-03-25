using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public class ProductVariantImageService : Service<ProductVariantImage>, IProductVariantImageService
    {
        public ProductVariantImageService(IGenericRepository<ProductVariantImage> repository) : base(repository)
        {
        }
    }
}

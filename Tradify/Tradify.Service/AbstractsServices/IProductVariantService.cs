using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.AbstractsRepositories;

namespace Tradify.Service.AbstractsServices
{
    public interface IProductVariantService : IProductVariantRepository
    {
        public Task<(string, int?)> AddProductVariantAsync(ProductVariants variants); //, int storeid);
    }
}

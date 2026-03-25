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
    public class ProductVariantImageRepository : GenericRepository<ProductVariantImage>, IProductVariantImageRepository
    {
        #region Filds
        private DbSet<ProductVariantImage> productVariantImages;
        #endregion

        #region Constructor
        public ProductVariantImageRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            productVariantImages = applicationDbContext.Set<ProductVariantImage>();
        }

        #endregion

        #region Methods

        #endregion
    
    }
}

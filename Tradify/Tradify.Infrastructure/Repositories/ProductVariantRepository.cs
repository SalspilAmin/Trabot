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
    public class ProductVariantRepository : GenericRepository<ProductVariants>, IProductVariantRepository
    {
        #region Filds
        private DbSet<ProductVariants> ProductVariants;
        #endregion

        #region Constructor
        public ProductVariantRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            ProductVariants = applicationDbContext.Set<ProductVariants>();
        }

        #endregion

        #region Methods

        #endregion
    
    }
}

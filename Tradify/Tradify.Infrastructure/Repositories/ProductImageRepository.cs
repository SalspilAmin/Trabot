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
    public class ProductImageRepository : GenericRepository<ProductImage>, IProductImagRepository
    {
        #region Filds
        private DbSet<ProductImage> productsImages;
        #endregion

        #region Constructor
        public  ProductImageRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            productsImages = applicationDbContext.Set<ProductImage>();
        }

        #endregion

        #region Methods

        #endregion

    }
}

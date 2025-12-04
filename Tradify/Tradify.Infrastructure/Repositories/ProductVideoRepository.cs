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
    public class ProductVideoRepository : GenericRepository<ProductVideo>, IProductVideoRepository
    {
        #region Filds
        private DbSet<ProductVideo> ProductVideos;
        #endregion

        #region Constructor
        public ProductVideoRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            ProductVideos = applicationDbContext.Set<ProductVideo>();
        }

        #endregion

        #region Methods

        #endregion

    }
}

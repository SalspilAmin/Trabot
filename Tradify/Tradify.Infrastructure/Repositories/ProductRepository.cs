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
    public class ProductRepository : GenericRepository<Products>, IProductRepository
    {
        #region Filds
        private DbSet<Products> products;
        #endregion

        #region Constructor
        public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
             products  = applicationDbContext.Set<Products>();
        }

        #endregion

        #region Methods

        #endregion

    }
}

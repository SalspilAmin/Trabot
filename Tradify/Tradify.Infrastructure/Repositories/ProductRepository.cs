using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.Context;
using Tradify.Infrastructure.InfrastrucureBases;
using static Tradify.Data.AppMetaData.Router;

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
            products = applicationDbContext.Set<Products>();
        }

        #endregion

        #region Methods

        public async Task<Products?> GetByIdWithIncludesAsync(int id)
        {
            return await products
                .Include(p => p.Reviews)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public  IQueryable<Products> GetProductsByCategoryAsync(int categoryId)
        {
            return  products
                .Where(p => p.CategoryId == categoryId)//&& p.Store.IsActive
                .Include(p => p.ProductImages)
                 .Include(p => p.Reviews)
                 .AsNoTracking();
        }
        #endregion
    }
}


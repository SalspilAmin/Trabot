using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.AbstractsRepositories;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Service.AbstractsServices
{
    public interface IProductService : IProductRepository
    {
        Task<Products?> GetByIdWithIncludesAsync(int id);
        IQueryable<Products> GetProductsByCategoryAsync(int categoryId);

    }
}

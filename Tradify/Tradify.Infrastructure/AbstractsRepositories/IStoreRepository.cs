using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.InfrastrucureBases;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Infrastructure.AbstractsRepositories
{
    public interface IStoreRepository : IGenericRepository<Stores>
    {
        Task<Stores?> GetByIdWithIncludesAsync(int id);
        IQueryable<Stores?> GetTableIgnoreQueryFilters();
        Task<Stores?> GetBySellerIdAsync(int sellerId);
    }
}

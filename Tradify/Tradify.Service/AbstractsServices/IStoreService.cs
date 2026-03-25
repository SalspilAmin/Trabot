using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.AbstractsRepositories;

namespace Tradify.Service.AbstractsServices
{
    public interface IStoreService : IStoreRepository
    {
        Task<Stores?> GetByIdWithIncludesAsync(int id);
        IQueryable<Stores?> GetTableIgnoreQueryFilters();
        Task<Stores?> GetBySellerIdAsync(int sellerId);


    }
}

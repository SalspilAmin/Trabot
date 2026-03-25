using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Infrastructure.Repositories;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public class StoreService : Service<Stores>, IStoreService
    {
        //public StoreService(IGenericRepository<Stores> repository) : base(repository)
        //{
        //}

        private readonly IStoreRepository storeRepository;

        public StoreService(IStoreRepository repository)
            : base(repository)
        {
            storeRepository = repository;
        }
        public async Task<Stores> GetByIdWithIncludesAsync(int id)
        {
            try
            {
                return await storeRepository.GetByIdWithIncludesAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public IQueryable<Stores?> GetTableIgnoreQueryFilters()
        {
            try
            {
                return  storeRepository.GetTableIgnoreQueryFilters();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Stores?> GetBySellerIdAsync(int sellerId)
        {
            try
            {
                return await storeRepository.GetBySellerIdAsync(sellerId);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}

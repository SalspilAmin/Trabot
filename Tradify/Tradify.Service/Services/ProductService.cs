using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Service.Services
{
    public class ProductService : Service<Products>, IProductService
    {
        //public ProductService(IGenericRepository<Products> repository) : base(repository)
        //{
        //}
        
            private readonly IProductRepository productRepository;

            public ProductService(IProductRepository repository)
                : base(repository)
            {
                productRepository = repository;
            }

            public async Task<Products> GetByIdWithIncludesAsync(int id)
            {
            try
            {
                return await productRepository.GetByIdWithIncludesAsync(id);
            }
            catch (Exception ex)
            {
                throw;
            }
            }
   
    }
}


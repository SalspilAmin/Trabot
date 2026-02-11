using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.InfrastrucureBases;
using Tradify.Service.AbstractsServices;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.Services
{
    public class CartService : Service<Cart>, ICartService
    {
        private readonly ICartRepository cartRepository;
        public CartService(IGenericRepository<Cart> repository,ICartRepository cartRepository) : base(repository)
        {
            this.cartRepository = cartRepository;
        }

        public Cart? GetCartByIdWithInclude(int id)
        {
            var result = cartRepository.GetCartByIdWithInclude(id);

            return result;
        }
    }
}

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
    public class CartRepository : GenericRepository<Cart>,ICartRepository
    {
        private ApplicationDbContext applicationDb;
        public CartRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            this.applicationDb = applicationDbContext;
        }

        public Cart? GetCartByIdWithInclude(int id)
        {
            var  cart = applicationDb.Carts.Include(c=>c.CartProducts).ThenInclude(x=>x.Product).FirstOrDefault(x=>x.Id==id);
            return cart;
        }
    }
}

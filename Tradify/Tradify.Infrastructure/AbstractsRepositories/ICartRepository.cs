using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.InfrastrucureBases;

namespace Tradify.Infrastructure.AbstractsRepositories
{
    public interface ICartRepository : IGenericRepository<Cart>
    {
        public Cart? GetCartByIdWithInclude(int id);

    }
}

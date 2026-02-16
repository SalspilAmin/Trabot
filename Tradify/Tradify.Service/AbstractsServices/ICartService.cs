using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Service.ServiceBases;

namespace Tradify.Service.AbstractsServices
{
    public interface ICartService : IService<Cart>
    {
        public Cart? GetCartByIdWithInclude(int id);
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.CartMapping
{
    public partial class CartProfile : Profile
    {
        public CartProfile() 
        {

         GetCartByUserIdMappint();
        }
    }
}

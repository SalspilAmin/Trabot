using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Cart.Queries.Results;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.CartMapping
{
    public partial class CartProfile
    {
        public void GetCartByUserIdMappint()
        {
            CreateMap<Cart, GetCartByUserIdQueryResult>()
                .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ProductsInCart, opt => opt.MapFrom(src => src.CartProducts));

            CreateMap<CartProduct, CartProductResult>();
        }
    }
}

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

            CreateMap<CartProduct, CartProductResult>()
                .ForMember(dest => dest.ProductVariant,
                    opt => opt.MapFrom(src => src.ProductVariant))
                .ForMember(dest=>dest.Quantity,opt=>opt.MapFrom(src=>src.Quantity));

            CreateMap<ProductVariants, ProductVariantResult>()
    .ForMember(dest => dest.ProductVariantName,
        opt => opt.MapFrom(src => src.ProductVarintName))

    .ForMember(dest => dest.InStock,
        opt => opt.MapFrom(src => src.InStock))

    .ForMember(dest => dest.FinalPrice,
        opt => opt.MapFrom(src => src.FinalPrice))

    .ForMember(dest => dest.ImageUrl,
        opt => opt.MapFrom(src =>
            src.ProductVariantImage != null
                ? src.ProductVariantImage.MediaPath
                : null));
        }
    }
}

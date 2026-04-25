using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.ProductMapping
{
    public partial class ProductProfile
    {
        public void GetProductDiscountMapping() 
        {
            CreateMap<Products, GetProductDiscountResponse>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))
                    .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.ProductImages.FirstOrDefault(i => i.IsMain)))


                    .ForMember(dest => dest.Discount, opt => opt.MapFrom(x => x.ProductVariants != null && x.ProductVariants.Any() ? x.ProductVariants.Max(v => v.Discount) : 0));

        }

    }
}

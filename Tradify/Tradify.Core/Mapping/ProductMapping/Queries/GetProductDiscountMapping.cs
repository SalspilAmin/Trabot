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
                    .ForMember(dest => dest.MainImage, opt => opt.MapFrom(src => src.ProductImages
                            .OrderByDescending(i => i.IsMain)
                            .Select(i => i.MediaPath)
                            .FirstOrDefault()));

            //.ForMember(dest=>dest.Discount , opt=>opt.MapFrom (x=>x.ProductVariants!=null && x.ProductVariants.Any()?x.ProductVariants.)
            //     .ForMember(dest => dest.FinalPrice, src => src.MapFrom(x => x.ProductVariants != null && x.ProductVariants.Any() ? x.ProductVariants.Min(v => v.FinalPrice) : 0))

        }

    }
}

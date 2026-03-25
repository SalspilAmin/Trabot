using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.ProductVariant.Queries.Results;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.ProductVariantMapping
{
    public partial class ProductVariantProfile
    {
        public void GetProductVariantPaginationMapping()
        {
            CreateMap<ProductVariants, GetProductVariantByProductResponse>()
           .ForMember(dest => dest.FinalPrice, op => op.MapFrom(src=>src.FinalPrice))

           
           .ForMember(dest => dest.MainImage,
               opt => opt.MapFrom(src =>
                    src.ProductVariantImages
                       .Where(i => i.IsMain)
                       .Select(i => i.MediaPath)
                       .FirstOrDefault()
                       ?? src.ProductVariantImages
                       .Select(i => i.MediaPath)
                       .FirstOrDefault()
               ));
          
            

        }
    }
}

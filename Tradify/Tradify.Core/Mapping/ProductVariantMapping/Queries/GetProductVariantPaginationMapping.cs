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


          .ForMember(dest => dest.Image, opt => opt.MapFrom(x => x.ProductVariantImage));

            CreateMap<ProductVariantImage, ProductVariantImageResponse>().ForMember(dest => dest.MediaPath,
                    opt => opt.MapFrom(src => src.MediaPath));



        }
    }
}

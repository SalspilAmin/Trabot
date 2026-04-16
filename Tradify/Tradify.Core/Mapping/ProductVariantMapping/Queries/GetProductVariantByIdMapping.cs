using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Features.ProductVariant.Queries.Results;
using Tradify.Core.Features.Store.Queries.Results;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.ProductVariantMapping
{
    public partial class ProductVariantProfile
    {
        public void GetProductVariantByIdMapping()
        {
            CreateMap<ProductVariants, GetProductVariantByIdResponse>()
                   
                    .ForMember(dest => dest.Image, opt => opt.MapFrom(x => x.ProductVariantImage));

            //CreateMap<ProductVariantImage, ProductVariantImageResponse>().ForMember(dest => dest.MediaPath,
            //        opt => opt.MapFrom(src => src.MediaPath));


        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Features.ProductVariant.Queries.Results;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.ProductVariantMapping
{
    public partial class ProductVariantProfile
    {
        public void GetProductVariantByIdMapping() {
            CreateMap<ProductVariants, GetProductVariantByIdResponse>()
                    .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product != null ? src.Product.Name : string.Empty))
                   .ForMember(dest => dest.Images,
                              opt => opt.MapFrom(src => src.ProductVariantImages.OrderByDescending(i => i.IsMain)
                                                                                 .ThenBy(i => i.Id)))

                    .ForMember(dest => dest.MetaData, opt => opt.MapFrom(src =>
                        !string.IsNullOrEmpty(src.MetaData)
                            ? JsonSerializer.Deserialize<Dictionary<string, string>>(src.MetaData)
                            : null
                    ));

            CreateMap<ProductVariantImage, ProductVariantImageResponse>().ForMember(dest => dest.MediaPath,
                    opt => opt.MapFrom(src => src.MediaPath));

            
        }
    
    }
}

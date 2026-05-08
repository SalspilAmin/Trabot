using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Helpers.Fawaterak;

namespace Tradify.Core.Mapping.FawaterakMapping
{
    public partial class FawaterakProfile
    {
        public void ProductMapping()
        {
            CreateMap<ProductVariants, ProductINRequestApi>()
                .ForMember(dest => dest.name, opt => opt.MapFrom(src => src.Name))
               // .ForMember(dest => dest.price, opt => opt.MapFrom(src => src.Price))
                ;
        }
    }
}

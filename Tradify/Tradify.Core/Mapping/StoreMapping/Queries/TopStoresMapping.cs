using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Tradify.Core.Features.Dashpoard.Admin.Queries.Results;
using Tradify.Core.Features.Store.Queries.Results;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.StoreMapping
{

    public partial class StoreProfile
    {
        public void TopStoresMapping()
        {
            CreateMap<Stores, TopStoresResponse>()

                     .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))

                    .ForMember(dest => dest.Name, src => src.MapFrom(x => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.Name.ToLower())))
                    .ForMember(dest => dest.Type, src => src.MapFrom(x => x.Type.ToString()))


                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))

                    .ForMember(dest => dest.Image, opt => opt.MapFrom(x => x.StoreImage));
        }
    }
}

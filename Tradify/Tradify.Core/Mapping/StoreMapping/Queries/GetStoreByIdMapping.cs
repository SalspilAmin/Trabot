using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Features.Store.Queries.Results;
using Tradify.Data.Entities;
using Tradify.Service.AbstractsServices;

namespace Tradify.Core.Mapping.StoreMapping
{

    public partial class StoreProfile 
    {

        
        public void GetStoreByIdMapping()
        {
            CreateMap<Stores, GetStoreByIdResponse>()

                    .ForMember(dest => dest.Name, src => src.MapFrom(x =>CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.Name.ToLower())))

                    .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description))
                    .ForMember(dest => dest.Type, src => src.MapFrom(x => x.Type.ToString()))


                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))

                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                      .ForMember(dest => dest.BusinessType, opt => opt.MapFrom(src => src.Seller != null ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(src.Seller.BusinessType.ToLower()) : ""))


                    
                     .ForMember(dest => dest.Image, opt => opt.MapFrom(x => x.StoreImage));


            CreateMap<StoreImage, StoreImageResponse>()
              .ForMember(dest => dest.MediaPath,
                  opt => opt.MapFrom(src => src.MediaPath));
        }
        
    }
}

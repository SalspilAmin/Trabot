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

                    .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))

                    .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description))

                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))

                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
                    .ForMember(dest => dest.BusinessName, opt => opt.MapFrom(src => src.Seller != null ? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(src.Seller.BusinessName.ToLower()) : ""))

                    //.ForMember(dest => dest.BusinessName, opt => opt.MapFrom(src => src.Seller != null ? src.Seller.BusinessName : ""))
                   
            //.ForMember(dest => dest.Images,
                    //opt => opt.MapFrom(src => src.StoreImages.OrderByDescending(i => i.IsMain).ThenBy(i => i.Id)));
                     .ForMember(dest => dest.Image, opt => opt.MapFrom(x => x.StoreImage));


            CreateMap<StoreImage, StoreImageResponse>()
              .ForMember(dest => dest.MediaPath,
                  opt => opt.MapFrom(src => src.MediaPath));
        }
        
    }
}

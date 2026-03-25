using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Features.Store.Queries.Results;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.StoreMapping
{
    public partial class StoreProfile 
    {
        public void GetStoreByIdMapping()
        {
            CreateMap<Stores, GetStoreByIdResponse>()

                    .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))

                    .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description))

                    .ForMember(dest => dest.IsActive,opt => opt.MapFrom(src => src.IsActive))
                   
                    .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))

                    .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Seller != null ? src.Seller.User.UserName : "")); 

          
        }

    }
}

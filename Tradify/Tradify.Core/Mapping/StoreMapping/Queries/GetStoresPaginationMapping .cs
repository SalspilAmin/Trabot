using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Store.Commands.Models;
using Tradify.Core.Features.Store.Queries.Models;
using Tradify.Core.Features.Store.Queries.Results;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.StoreMapping
{
    public partial class StoreProfile 
    {
        public void GetStoresPaginationMapping()
        {

            CreateMap<Stores, GetAllStoresResponse>()
                    .ForMember(dest => dest.Id, src => src.MapFrom(x => x.Id))

                    .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))


                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive))

                    .ForMember(dest => dest.Image, opt => opt.MapFrom(x => x.StoreImage));


            //CreateMap<StoreImage, StoreImageResponse>()
            //  .ForMember(dest => dest.MediaPath,
            //      opt => opt.MapFrom(src => src.MediaPath));


        }

    }
}

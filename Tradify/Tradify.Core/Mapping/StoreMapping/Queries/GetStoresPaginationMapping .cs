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

            CreateMap<Stores, GetStoresPaginationResponse>()

                    .ForMember(dest => dest.Name, src => src.MapFrom(x => x.Name))

                    .ForMember(dest => dest.Description, src => src.MapFrom(x => x.Description))

                    .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));

                    
        }

    }
}

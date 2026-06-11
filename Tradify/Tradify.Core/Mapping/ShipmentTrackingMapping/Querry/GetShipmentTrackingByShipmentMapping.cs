using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.ShipmentTrackings.Queries.Results;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.ShipmentTrackingMapping
{
    public partial class ShipmentProfile
    {
        public void GetShipmentTrackingByShipmentMapping()
        {
            


            CreateMap<ShipmentTracking, GetShipmentTrackingByShipmentResponse>()
    .ForMember(dest => dest.Id,
        opt => opt.MapFrom(src => src.Id))

    .ForMember(dest => dest.ShipmentStatus,
        opt => opt.MapFrom(src => src.ShipmentStatus))

    .ForMember(dest => dest.CreatedAt,
        opt => opt.MapFrom(src => src.CreatedAt))

    .ForMember(dest => dest.Notes,
        opt => opt.MapFrom(src => src.Notes));


        }
    }
}


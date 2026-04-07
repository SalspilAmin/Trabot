using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Order.Queries.Results;
using Tradify.Core.Features.ShipmentTrackings.Queries.Results;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.ShipmentTrackingMapping
{
    public partial class ShipmentProfile 
    {
        public void GetShipmentByOrderIdMapping()
        {
            CreateMap<ShipmentTracking, GetShipmentByOrderIdResponse>()
             .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.ShipmentStatus));

        }
    }
}

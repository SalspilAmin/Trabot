using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.ShipmentTrackings.Queries.Results;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.ShipmentTrackingMapping
{
    public partial class ShipmentProfile
    {

        public void GetSellerShipmentMapping()
        {

            CreateMap<Shipments, GetSellerShipmentResponse>()
             .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))


            .ForMember(dest => dest.TrackingNumber, opt => opt.MapFrom(src => src.TrackingNumber))

               .ForMember(dest => dest.CurrentStatus, opt => opt.MapFrom(src => src.CurrentStatus));



        }
    }
}

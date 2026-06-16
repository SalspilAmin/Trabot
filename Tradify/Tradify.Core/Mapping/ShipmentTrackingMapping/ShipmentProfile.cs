using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.ShipmentTrackingMapping
{
    public partial class ShipmentProfile : Profile
    {
        public ShipmentProfile()
        {
            GetShipmentByOrderIdMapping();
            GetSellerShipmentMapping();
            GetShipmentTrackingByShipmentMapping();
        }
    }
    
}

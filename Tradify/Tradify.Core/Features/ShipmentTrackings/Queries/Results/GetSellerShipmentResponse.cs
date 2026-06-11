using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.ShipmentTrackings.Queries.Results
{
    public class GetSellerShipmentResponse
    {
        public int Id { get; set; }

        public string TrackingNumber { get; set; }
        public ShipmentStatus CurrentStatus { get; set; }
    }
}

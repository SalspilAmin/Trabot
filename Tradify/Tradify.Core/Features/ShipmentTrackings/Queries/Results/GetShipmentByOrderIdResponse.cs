using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.ShipmentTrackings.Queries.Results
{
    public class GetShipmentByOrderIdResponse
    {
        public string TrackingNumber { get; set; }
        public ShipmentStatus Status { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

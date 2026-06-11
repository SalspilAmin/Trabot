using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.ShipmentTrackings.Queries.Results
{
    public class GetShipmentTrackingByShipmentResponse
    {
        public int Id { get; set; }

        public ShipmentStatus ShipmentStatus { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? Notes { get; set; }
    }
}

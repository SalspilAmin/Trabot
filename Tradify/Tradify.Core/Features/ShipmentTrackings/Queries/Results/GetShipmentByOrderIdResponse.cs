using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Order.Queries.Results;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.ShipmentTrackings.Queries.Results
{
    public class GetShipmentByOrderIdResponse
    {
        public int Id { get; set; }

        public string TrackingNumber { get; set; }
        public ShipmentStatus CurrentStatus { get; set; }

        public ICollection<ShipmentTrackingRespons>? ShipmentTrackings { get; set; }

    }

    public class ShipmentTrackingRespons
    {
        public int Id { get; set; }

        public ShipmentStatus ShipmentStatus { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? Notes { get; set; }

    }
}

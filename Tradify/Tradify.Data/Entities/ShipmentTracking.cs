using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;

namespace Tradify.Data.Entities
{
    public  class ShipmentTracking
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string TrackingNumber { get; set; }

        public ShipmentStatus ShipmentStatus { get; set; }

        public DateTime UpdatedAt { get; set; }

    }
}

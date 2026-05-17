using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Enums;

namespace Tradify.Data.Entities
{
    public  class ShipmentTracking
    {
        public int Id { get; set; }

        public ShipmentStatus ShipmentStatus { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string? Notes { get; set; }
        public int ShipmentId { get; set; }

        [ForeignKey(nameof(ShipmentId))]
        public virtual Shipments Shipment { get; set; }

       // public string TrackingNumber { get; set; }



    }
}

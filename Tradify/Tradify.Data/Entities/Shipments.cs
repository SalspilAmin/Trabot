using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using Tradify.Data.Enums;

namespace Tradify.Data.Entities
{


    public class Shipments
    {
        public int Id { get; set; }
        public string TrackingNumber { get; set; }

        public DateTime CreatedAt { get; set; }=DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public int SubOrderId { get; set; }

        public ShipmentStatus CurrentStatus { get; set; }


        [ForeignKey(nameof(SubOrderId))]
        public virtual SubOrders SubOrder { get; set; }


        public virtual ICollection<ShipmentTracking> ShipmentTrackings { get; set; } = new HashSet<ShipmentTracking>();


        //public int OrderId { get; set; }

        //[ForeignKey(nameof(OrderId))]
        //public virtual Orders Order { get; set; }

        //public int ShipmentTrackingId { get; set; }

        //[ForeignKey(nameof(ShipmentTrackingId))]    
        //public virtual ShipmentTracking ShipmentTracking { get; set; }  



    }
}

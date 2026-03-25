using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Enums;

namespace Tradify.Data.Entities
{
    public class SubOrders
    {

        public int Id { get; set; } 

        public int OrderId { get; set; }

        public int StoreId { get; set; }

        public int? ShipmentId { get; set; }

        public int ShipmentTrackingId { get; set; }


        public OrderStatus Status { get; set; } 
         

        public DateTime CreatedAt { get; set; }


        [ForeignKey(nameof(OrderId))]
        public virtual Orders? Order {  get; set; }
        public virtual ICollection<OrderItems>? OrderItems { get; set; }

        [ForeignKey(nameof(ShipmentId))]
        public virtual Shipments? Shipment { get; set; }
        [ForeignKey(nameof(ShipmentTrackingId))]
        public virtual ShipmentTracking? ShipmentTracking { get; set; }



    }
}

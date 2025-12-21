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
        public Orders? Order {  get; set; }
        public virtual ICollection<Products>? Products { get; set; }
        [ForeignKey(nameof(ShipmentId))]
        public Shipments? Shipment { get; set; }
        [ForeignKey(nameof(ShipmentTrackingId))]
        public ShipmentTracking? ShipmentTracking { get; set; }



    }
}

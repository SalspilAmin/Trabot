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

        public int ShipmentTrackingId {  get; set; }

        //public int SubOrderId { get; set; }


        //[ForeignKey(nameof(SubOrderId))]
        //public virtual SubOrders SubOrder { get; set; }


        public int OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual Orders Order { get; set; }
        [ForeignKey(nameof(ShipmentTrackingId))]    
        public virtual ShipmentTracking ShipmentTracking { get; set; }  
        public DateTime UpdatedAT { get; set; }

        
        
    }
}

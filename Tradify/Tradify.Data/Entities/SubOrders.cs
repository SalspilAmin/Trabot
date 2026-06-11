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

        public int ProductId { get; set; }
     

        public int Quantity { get; set; }
        public int ProductVAriantsId { get; set; }



        public OrderStatus Status { get; set; } 
         

        public DateTime CreatedAt { get; set; }= DateTime.Now;


        [ForeignKey(nameof(OrderId))]
        
        public virtual Orders? Order { get; set; }
        public virtual ICollection<OrderItems>? OrderItems { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Products Product { get; set; }
        [ForeignKey(nameof(ProductVAriantsId))]
        public virtual ProductVariants ProductVariants { get; set; }

        public virtual Shipments? Shipment { get; set; }


        [ForeignKey(nameof(StoreId))]
        public virtual Stores Store { get; set; }

       

    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Enums;

namespace Tradify.Data.Entities
{


    public class Orders
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string ShippingAddress { get; set; } 

        public OrderStatus? OrderStatus { get; set; }

        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        public virtual List<Products> Products { get; set; }

        public decimal? ShippingPrice { get; set; }
        public decimal? TotalAmount { get; set; }

        public DateTimeOffset CreatedAt{ get; set; }= DateTimeOffset.Now;

        public DateTimeOffset EstimatedDelevery { get; set; } = DateTimeOffset.Now.AddDays(1);
        public long? invoice_id { get; set; }
        public string? invoice_key { get; set; }
        public int CartId { get; set; }
        [ForeignKey(nameof(CartId))]
        public virtual Cart cart { get; set; }

        [ForeignKey(nameof(CustomerId))]
        public User? User { get; set; }

      public virtual  ICollection<Products>? products { get; set; }

        public virtual ICollection<SubOrders>? subOrders { get; set; }   
    }
}

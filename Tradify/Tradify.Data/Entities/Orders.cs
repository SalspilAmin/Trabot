using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;

namespace Tradify.Data.Entities
{


    public class Orders
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string ShippingAddress { get; set; } 

        public OrderStatus OrderStatus { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public List<Products> Products { get; set; }

        public decimal? ShippingPrice { get; set; }
        public decimal? TotalAmount { get; set; }

        public DateTimeOffset CreatedAt{ get; set; }= DateTimeOffset.Now;

        public DateTime EstimatedDelevery { get; set; }
    }
}

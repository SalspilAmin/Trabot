using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.Order.Queries.Results
{
    public class OrderResultQueiry
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string ShippingAddress { get; set; }

        public OrderStatus? OrderStatus { get; set; }

        public PaymentStatus PaymentStatus { get; set; } 
        public long? invoice_id { get; set; }
        public string? invoice_key { get; set; } = null;
        public decimal? ShippingPrice { get; set; }
        public decimal? TotalAmount { get; set; }

        public List<orderItemResult>? orderItems { get; set; }


        public class orderItemResult
        {
            public int Id { get; set; }

            public int SuborderId { get; set; }
            public int ProductVariantId { get; set; }

            public int Quantity { get; set; }

            public decimal Price { get; set; }
        }
    }
    
}

using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.Order.Queries.Results
{
    public class GetSupOrderByOrderIdResponse
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string StoreName { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<OrderItemsRespons>? ProductVarints { get; set; }



    }
    public class OrderItemsRespons
    {
        public int ProductVariantId { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }


        public decimal Price { get; set; }
    }
}

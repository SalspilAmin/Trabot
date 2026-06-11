using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.Order.Queries.Results
{
    public class GetCustomerOrdersResponse
    {
        public int OrderId { get; set; }
        public decimal TotalPrice { get; set; }

        public string? OrderStatus { get; set; }

        public int TotalSubOrders { get; set; }

        public int DeliveredSubOrders { get; set; }

        public DateTimeOffset CreatedAt { get; set; }

    }
}

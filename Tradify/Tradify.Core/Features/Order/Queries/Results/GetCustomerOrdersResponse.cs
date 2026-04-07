using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.Order.Queries.Results
{
    public class GetCustomerOrdersResponse
    {
        public int OrderId { get; set; }
        public decimal? TotalPrice { get; set; }
        public OrderStatus? OrderStatus { get; set; }
        public DateTimeOffset CreatedAt { get; set; }

    }
}

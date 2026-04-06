using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Order.Queries.Results;

namespace Tradify.Core.Mapping.Orders
{
    public partial class OrderMapping
    {
        public void OrderItemResultMapping()
        {
            CreateMap<Tradify.Data.Entities.OrderItems, OrderResultQueiry.orderItemResult>();

        }
    }

}

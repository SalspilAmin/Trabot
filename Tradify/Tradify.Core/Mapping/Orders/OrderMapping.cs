using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.Orders
{
    public partial class OrderMapping : Profile
    {
        public OrderMapping() 
        {
            CreateOrderMapping();
            GetOrderByIdMapping();
            OrderItemResultMapping();
        }
    }
}

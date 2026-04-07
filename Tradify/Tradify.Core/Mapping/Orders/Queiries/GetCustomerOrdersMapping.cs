using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Order.Queries.Results;

namespace Tradify.Core.Mapping.Orders
{
    public partial class OrderMapping
    {

        public void GetCustomerOrdersMapping() 
        {
            CreateMap<Tradify.Data.Entities.Orders, GetCustomerOrdersResponse>()
               .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalAmount))
               .ForMember(dest => dest.OrderStatus, opt => opt.MapFrom(src => src.OrderStatus.ToString()));

        }

    }
}

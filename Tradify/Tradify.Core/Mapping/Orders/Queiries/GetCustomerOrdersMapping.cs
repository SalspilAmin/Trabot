using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Order.Queries.Results;
using Tradify.Data.Enums;

namespace Tradify.Core.Mapping.Orders
{
    public partial class OrderMapping
    {

        public void GetCustomerOrdersMapping() 
        {
            CreateMap<Tradify.Data.Entities.Orders, GetCustomerOrdersResponse>()
               .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalAmount))
.ForMember(dest => dest.OrderStatus,
    opt => opt.MapFrom(src =>
        src.OrderStatus.HasValue
            ? src.OrderStatus.ToString()
            : null))
                .ForMember(dest => dest.TotalSubOrders, opt => opt.MapFrom(src => src.subOrders.Count))

                       .ForMember(dest => dest.DeliveredSubOrders,
           opt => opt.MapFrom(src =>
               src.subOrders.Count(s =>
                   s.Status == OrderStatus.delivered)))


                        .ForMember(dest => dest.CreatedAt,
           opt => opt.MapFrom(src => src.CreatedAt));

        }

    }
}

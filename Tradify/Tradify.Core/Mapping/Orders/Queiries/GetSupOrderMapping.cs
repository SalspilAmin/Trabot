using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Order.Queries.Results;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.Orders
{
    public partial class OrderMapping
    {

        public void GetSupOrderMapping()
        {
            CreateMap<Tradify.Data.Entities.SubOrders, GetSupOrderByOrderIdResponse>()

               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))

               .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.OrderId))

               .ForMember(dest => dest.StoreName, opt => opt.MapFrom(src => src.Store.Name))

               .ForMember(dest => dest.Status,opt => opt.MapFrom(src =>src.Status.ToString()))


                        .ForMember(dest => dest.CreatedAt,
           opt => opt.MapFrom(src => src.CreatedAt))

                         .ForMember(dest => dest.ProductVarints,
        opt => opt.MapFrom(src => src.OrderItems));


            CreateMap<OrderItems, OrderItemsRespons>()
    .ForMember(dest => dest.ProductVariantId,
        opt => opt.MapFrom(src => src.ProductVariantId))

    .ForMember(dest => dest.Name,
        opt => opt.MapFrom(src => src.ProductVariant.Name))

    .ForMember(dest => dest.Quantity,
        opt => opt.MapFrom(src => src.Quantity))

    .ForMember(dest => dest.Price,
        opt => opt.MapFrom(src => src.Price));


        }

    }
}

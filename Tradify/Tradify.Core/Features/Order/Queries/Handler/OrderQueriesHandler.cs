using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Order.Queries.Models;
using Tradify.Core.Features.Order.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Service.AbstractsServices;

namespace Tradify.Core.Features.Order.Queries.Handler
{
    public class OrderQueriesHandler : ResponseHandler, IRequestHandler<GetOrderByIdQueiry, Response<OrderResultQueiry>>
    {

        private readonly LocalizationService localization;
        private readonly IOrdersService ordersService;
        private readonly ICartService cartService;
        private readonly ICartProductService cartProductService;
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public OrderQueriesHandler(LocalizationService localization, IOrdersService ordersService, ICartService cartService, ICartProductService cartProductService, IProductService productService, IMapper mapper) : base(localization)
        {

            this.localization = localization;
            this.ordersService = ordersService;
            this.cartService = cartService;
            this.cartProductService = cartProductService;
            this.productService = productService;
            this.mapper = mapper;
        }

        public async Task<Response<OrderResultQueiry>> Handle(GetOrderByIdQueiry request, CancellationToken cancellationToken)
        {
            var order = await ordersService.GetByIdAsync(request.Id);
            if (order == null) return BadRequest<OrderResultQueiry>(localization.Get("NotFound"));
            var orderItems = order.OrderItems.ToList();
            if(orderItems.Count == 0) return BadRequest<OrderResultQueiry>(localization.Get("OrderItemsNotFound"));
            var result = mapper.Map<OrderResultQueiry>(order);
            foreach (var item in orderItems)
            {
                result.orderItems.Add(mapper.Map<OrderResultQueiry.orderItemResult>(item));

            }

            return Success<OrderResultQueiry>(result);

        }
    }
}

using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Order.Queries.Models;
using Tradify.Core.Features.Order.Queries.Results;
using Tradify.Core.Features.User.Queries.Models;
using Tradify.Core.Features.User.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Data.Entities.Identity;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.Order.Queries.Handler
{
    public class OrderQueriesHandler : ResponseHandler, IRequestHandler<GetOrderByIdQueiry, Response<OrderResultQueiry>>
                                                      , IRequestHandler<GetCustomerOrdersQuery, PaginatedResult<GetCustomerOrdersResponse>>
    {

        private readonly LocalizationService localization;
        private readonly IOrdersService ordersService;
        private readonly ICartService cartService;
        private readonly ICartProductService cartProductService;
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly ICurrentUserService currentUserService;


        public OrderQueriesHandler(LocalizationService localization, ICurrentUserService currentUserService, IOrdersService ordersService, ICartService cartService, ICartProductService cartProductService, IProductService productService, IMapper mapper) : base(localization)
        {

            this.localization = localization;
            this.ordersService = ordersService;
            this.cartService = cartService;
            this.cartProductService = cartProductService;
            this.productService = productService;
            this.mapper = mapper;
            this.currentUserService = currentUserService;   
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
   
        public async Task<PaginatedResult<GetCustomerOrdersResponse>> Handle( GetCustomerOrdersQuery request,CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();

            // 1️⃣ Query
            var orders =  ordersService.GetTableNoTracking()
                .Where(o => o.CustomerId == userId)
                .OrderByDescending(o => o.CreatedAt).AsQueryable();

            // 2️⃣ Pagination

            var result = await mapper.ProjectTo<GetCustomerOrdersResponse>(orders)
                .ToPaginationlist(request.PageNumber, request.PageSize);

           

            return result;
        }
    }
}

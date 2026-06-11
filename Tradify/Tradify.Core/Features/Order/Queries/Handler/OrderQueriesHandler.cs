using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.InstructorService.Queries.Models;
using Tradify.Core.Features.InstructorService.Queries.Results;
using Tradify.Core.Features.Order.Queries.Models;
using Tradify.Core.Features.Order.Queries.Results;
using Tradify.Core.Features.User.Queries.Models;
using Tradify.Core.Features.User.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Enums;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.Order.Queries.Handler
{
    public class OrderQueriesHandler : ResponseHandler, IRequestHandler<GetOrderByIdQueiry, Response<OrderResultQueiry>>
                                                      , IRequestHandler<GetCustomerOrdersQuery, PaginatedResult<GetCustomerOrdersResponse>>
                                                     , IRequestHandler<GetSupOrderByOrderIdQuery, List<GetSupOrderByOrderIdResponse>>
                                                      , IRequestHandler<GetSellerSupOrderQuery, Response<PaginatedResult<GetSupOrderByOrderIdResponse>>>


    {

        private readonly LocalizationService localization;
        private readonly IOrdersService ordersService;
        private readonly ICartService cartService;
        private readonly ICartProductService cartProductService;
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly ICurrentUserService currentUserService;
        private readonly ISubOrderService subOrderService;  

        public OrderQueriesHandler(LocalizationService localization, 
            ICurrentUserService currentUserService, 
            IOrdersService ordersService, 
            ICartService cartService, 
            ICartProductService cartProductService, 
            IProductService productService, 
            IMapper mapper,
            ISubOrderService subOrderService) : base(localization)
        {

            this.localization = localization;
            this.ordersService = ordersService;
            this.cartService = cartService;
            this.cartProductService = cartProductService;
            this.productService = productService;
            this.mapper = mapper;
            this.currentUserService = currentUserService;
            this.subOrderService = subOrderService; 
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
                .Include(x => x.subOrders)
                .OrderByDescending(o => o.CreatedAt).AsQueryable();

            // 2️⃣ Pagination

            var result = await mapper.ProjectTo<GetCustomerOrdersResponse>(orders)
                .ToPaginationlist(request.PageNumber, request.PageSize);

           

            return result;
        }


        public async Task<Response<PaginatedResult<GetSupOrderByOrderIdResponse>>> Handle(GetSellerSupOrderQuery request, CancellationToken cancellationToken)
        {
            var ValidSeller = await currentUserService.GetValidSellerContextAsync();

            if (ValidSeller.Error != null)
                return BadRequest <PaginatedResult<GetSupOrderByOrderIdResponse>>(localization.Get(ValidSeller.Error));



            // 2. Get Seller , Store
            var seller = ValidSeller.Seller;
            var store = ValidSeller.Store;

            //3. Cheack If Store Type Is Product 

            if (store.Type != StoreType.Product)
                return BadRequest<PaginatedResult<GetSupOrderByOrderIdResponse>>(localization.Get("ThisActionAllowedForProductStoresOnly"));



            // 1️⃣ Query
            var suporders = subOrderService.GetTableNoTracking()
                .Where(s=>s.StoreId == store.Id)
                .Include(x => x.OrderItems)
                  .ThenInclude(x => x.ProductVariant)
                .OrderByDescending(o => o.CreatedAt).AsQueryable();

            // 2️⃣ Pagination

            var result = await mapper.ProjectTo<GetSupOrderByOrderIdResponse>(suporders)
                .ToPaginationlist(request.PageNumber, request.PageSize);



            return Success (result);
        }

        public async Task<List<GetSupOrderByOrderIdResponse>> Handle(GetSupOrderByOrderIdQuery request, CancellationToken cancellationToken)
        {


            var supOrder = await subOrderService.GetTableNoTracking()
                  .Include(x => x.OrderItems)
                  .ThenInclude(x => x.ProductVariant)
                .Where(e => e.OrderId == request.OrderId).ToListAsync();



            var result = mapper.Map<List<GetSupOrderByOrderIdResponse>>(supOrder);

            return result;
        }

    }
}

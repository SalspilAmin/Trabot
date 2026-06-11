using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Order.Commands.Models;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Data.Enums;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;
using Twilio.Rest.Trunking.V1.Trunk;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Core.Features.Order.Commands.Handler
{
    public class OrderCommandHandler : ResponseHandler, IRequestHandler<CreateOrderModel, Response<int>>
                                                      ,IRequestHandler<UpdateOrderCommandModel,Response<int>>
                                                      ,IRequestHandler<DeleteOrderCommand, Response<string>>
    {
        private readonly LocalizationService localization;
        private readonly IOrdersService ordersService;
        private readonly ICartService cartService;
        private readonly ICartProductService cartProductService;
        private readonly IProductService productService;
        private readonly IMapper mapper;
        private readonly ICurrentUserService currentUserService;
        private readonly ISubOrderService subOrderService;  
        public OrderCommandHandler(LocalizationService localization,IOrdersService ordersService,ICartService cartService
            ,ICartProductService cartProductService,IProductService productService,IMapper mapper
            , ICurrentUserService currentUserService, ISubOrderService subOrderService) : base(localization) 
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

        //public async Task<Response<int>> Handle(CreateOrderModel request, CancellationToken cancellationToken)
        //{
        //    // Check on Cart  
        //    var Cart = cartService.GetCartByIdWithInclude(request.CartId);
        //    if (Cart == null) return BadRequest<int>(localization.Get("NotFound"));



        //    // list of product  that is in Cart
        //    var ProductsVariantsList = Cart.CartProducts.Select(x => x.ProductVariant).ToList();

        //    var order = mapper.Map<Tradify.Data.Entities.Orders>(request);
        //    if (order != null)
        //    {
        //        var subOrdersList=new List<SubOrders>();
        //        foreach (var item in ProductsVariantsList)
        //        {

        //            if (item.InStock != true || item.NumberOfProductInStock <= 0)
        //                continue;


        //                var cartproduct = Cart.CartProducts.FirstOrDefault(x => x.ProductVariantId == item.Id);

        //            // Check If SubOrder For This Store Already Exists
        //            var existingSubOrder = subOrdersList
        //                .FirstOrDefault(x => x.StoreId == item.Product.StoreId);
        //            // Create New SubOrder If Not Exists
        //            if (existingSubOrder == null)
        //            {
        //                existingSubOrder = new SubOrders
        //                {
        //                    Order = order
        //                    ,OrderId=order.Id,
        //                    StoreId = item.Product.StoreId,
        //                    Status = Data.Enums.OrderStatus.processing,
        //                    Quantity = cartproduct.Quantity,
        //                    ProductVAriantsId=item.Id,
        //                    ProductVariants=item,
        //                    Product=item.Product,
        //                    OrderItems = new List<Tradify.Data.Entities.OrderItems>()
        //                };

        //                subOrdersList.Add(existingSubOrder);
        //            }

        //            var orderItem = new OrderItems
        //            {
        //                ProductVariantId = item.Id,
        //                Quantity = cartproduct.Quantity,
        //                Price = item.Price,
        //                SubOrder = existingSubOrder
        //            };



        //                order.OrderItems.Add(orderItem);
        //            existingSubOrder.OrderItems.Add(orderItem);
        //            existingSubOrder.Quantity += cartproduct.Quantity;

        //            // Reduce Stock
        //            item.NumberOfProductInStock -= cartproduct.Quantity;




        //        }
        //        order.subOrders= subOrdersList;
        //    }
        //    var result = await ordersService.AddAsync(order);
        //    if (result != null)
        //    {

        //        await ordersService.SaveChangesAsync();

        //        return Success(result.Id);
        //    }
        //    return BadRequest<int>(localization.Get("TryAgainInAnotherTime"));





        //}
        public async Task<Response<int>> Handle(CreateOrderModel request, CancellationToken cancellationToken)
        {
            // Check on Cart
            var Cart = cartService.GetCartByIdWithInclude(request.CartId);
            if (Cart == null)
                return BadRequest<int>(localization.Get("NotFound"));

            // List of products in cart
            var ProductsVariantsList = Cart.CartProducts
                .Select(x => x.ProductVariant)
                .ToList();

            var order = mapper.Map<Tradify.Data.Entities.Orders>(request);

            if (order != null)
            {
                var subOrdersList = new List<SubOrders>();

                // Initialize collections if needed
                order.OrderItems ??= new List<OrderItems>();

                foreach (var item in ProductsVariantsList)
                {
                    if (item.InStock != true || item.NumberOfProductInStock <= 0)
                        continue;

                    var cartproduct = Cart.CartProducts
                        .FirstOrDefault(x => x.ProductVariantId == item.Id);

                    if (cartproduct == null)
                        continue;

                    // Check if SubOrder for this Store already exists
                    var existingSubOrder = subOrdersList
                        .FirstOrDefault(x => x.StoreId == item.Product.StoreId);

                    // Create new SubOrder if not exists
                    if (existingSubOrder == null)
                    {
                        existingSubOrder = new SubOrders
                        {
                            Order = order,
                            StoreId = item.Product.StoreId,
                            Status = Data.Enums.OrderStatus.processing,
                            Quantity = 0,
                            ProductVAriantsId = item.Id,
                            ProductVariants = item,
                            Product = item.Product,
                            OrderItems = new List<OrderItems>()
                        };

                        subOrdersList.Add(existingSubOrder);
                    }

                    var orderItem = new OrderItems
                    {
                        ProductVariantId = item.Id,
                        Quantity = cartproduct.Quantity,
                        Price = item.Price,
                        SubOrder = existingSubOrder
                    };

                    order.OrderItems.Add(orderItem);
                    existingSubOrder.OrderItems.Add(orderItem);
                    existingSubOrder.Quantity += cartproduct.Quantity;

                    // Reduce Stock
                    item.NumberOfProductInStock -= cartproduct.Quantity;
                }

                order.subOrders = subOrdersList;

                // Calculate Total Amount from Order Items
                order.ShippingPrice = request.ShippingPrice ?? 0;

                order.TotalAmount =
                    order.OrderItems.Sum(x => x.Price * x.Quantity)
                    + order.ShippingPrice;
            }

            var result = await ordersService.AddAsync(order);

            if (result != null)
            {
                await ordersService.SaveChangesAsync();
                return Success(result.Id);
            }

            return BadRequest<int>(localization.Get("TryAgainInAnotherTime"));
        }
        public async Task<Response<int>> Handle(UpdateOrderCommandModel request, CancellationToken cancellationToken)
        {
            var order = await ordersService.GetByIdAsync(request.Id);
            if (order == null) return BadRequest<int>(localization.Get("NotFound"));

            order.PaymentStatus = request.PaymentStatus;
            order.invoice_id = request.invoice_id;
            order.invoice_key = request.invoice_key;

            await ordersService.UpdateAsync(order);
            return Success(order.Id);
        }

      

        public async Task<Response<string>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {


            var userId = currentUserService.GetUserId();

            // 1️⃣ Get Order
            var order = await ordersService.GetTableAsTracking()
                .Include(o=>o.subOrders)
                .ThenInclude(s=>s.Shipment)
                .FirstOrDefaultAsync(o => o.Id == request.OrderId&&o.CustomerId== userId);


            if (order == null)
                return NotFound<string>(localization.Get("OrderNotFound"));

            

            // 2️⃣ Check Payment / Shipment
            if (order.PaymentStatus == PaymentStatus.Paid)
                return BadRequest<string>(localization.Get("CannotCancelledPaidOrder"));

            if (order.subOrders.Any(s => s.Shipment != null))
                return BadRequest<string>(localization.Get("OrderAlreadyShipped"));

            order.OrderStatus = OrderStatus.cancelled;
            order.PaymentStatus= PaymentStatus.Cancelled;    

            // 4️⃣ Delete Order
            await ordersService.UpdateAsync(order);

            var supOrder = order.subOrders.ToList();

                foreach (var sup in supOrder)
            { 
                sup.Status= OrderStatus.cancelled;
                await subOrderService.UpdateAsync(sup);

            }

            

            await ordersService.SaveChangesAsync();

            return Success(localization.Get("OrderCancelledSuccessfully"));
        }
    }
}

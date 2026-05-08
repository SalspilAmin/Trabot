using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
        public OrderCommandHandler(LocalizationService localization,IOrdersService ordersService,ICartService cartService
            ,ICartProductService cartProductService,IProductService productService,IMapper mapper
            , ICurrentUserService currentUserService) : base(localization) 
        {
            this.localization = localization;
            this.ordersService = ordersService;
            this.cartService = cartService;
            this.cartProductService = cartProductService;
            this.productService = productService;
            this.mapper = mapper;
            this.currentUserService = currentUserService;   
        }

        public async Task<Response<int>> Handle(CreateOrderModel request, CancellationToken cancellationToken)
        {
            // Check on Cart  
            var Cart = cartService.GetCartByIdWithInclude(request.CartId);
            if (Cart == null) return BadRequest<int>(localization.Get("NotFound"));



            // list of product  that is in Cart
            var ProductsVariantsList = Cart.CartProducts.Select(x => x.ProductVariant).ToList();

            var order = mapper.Map<Tradify.Data.Entities.Orders>(request);
            if (order != null)
            {
                var SubOrders=new List<SubOrders>();
                foreach (var item in ProductsVariantsList)
                {
                    
                    if (item.InStock == true)
                    {
                        var cartproduct = Cart.CartProducts.FirstOrDefault(x => x.ProductVariantId == item.Id);
                        var subOrders = new SubOrders { Order = order, OrderId = order.Id, Product = item.Product, StoreId = item.Product.StoreId, Quantity = cartproduct.Quantity, ProductVAriantsId = item.Id, ProductVariants = item, Status = Data.Enums.OrderStatus.processing };
                         SubOrders.Add(subOrders);
                        
                        var orderitem = new Tradify.Data.Entities.OrderItems { ProductVariantId = item.Id, SuborderId = subOrders.Id, Quantity = cartproduct.Quantity, Price = item.Price, SubOrder = subOrders };
                        order.OrderItems.Add(orderitem);
                        item.NumberOfProductInStock-=subOrders.Quantity;

                    }
                    

                }
                order.subOrders= SubOrders;
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

            
         // var userId = currentUserService.GetUserId();

            // 1️⃣ Get Order
            var order =  ordersService.GetTableAsTracking()
                .FirstOrDefault(o => o.Id == request.OrderId);
                //.FirstOrDefault(o => o.Id == request.OrderId && o.CanBeCanceled== userId);


            if (order == null)
                return NotFound<string>(localization.Get("OrderNotFound"));

            

            // 2️⃣ Check Payment / Shipment
            if (order.PaymentStatus == PaymentStatus.Paid)
                return BadRequest<string>(localization.Get("CannotDeletePaidOrder"));

            if (order.ShipmentId != null )
                return BadRequest<string>(localization.Get("OrderAlreadyShipped"));

            //// 3️⃣ Delete Order Items (if needed)
            //if (order.OrderItems != null && order.OrderItems.Count > 0)
            //{
            //    ordersService.RemoveRange(order.OrderItems);
            //}

            // 4️⃣ Delete Order
            await ordersService.DeleteAsync(order);
            await ordersService.SaveChangesAsync();

            return Success(localization.Get("OrderDeletedSuccessfully"));
        }
    }
}

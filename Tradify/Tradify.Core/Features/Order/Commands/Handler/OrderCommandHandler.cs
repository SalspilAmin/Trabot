using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Order.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;
using Twilio.Rest.Trunking.V1.Trunk;

namespace Tradify.Core.Features.Order.Commands.Handler
{
    public class OrderCommandHandler : ResponseHandler, IRequestHandler<CreateOrderModel, Response<int>>
    {
        private readonly LocalizationService localization;
        private readonly IOrdersService ordersService;
        private readonly ICartService cartService;
        private readonly ICartProductService cartProductService;
        private readonly IProductService productService;
        private readonly IMapper mapper;
        public OrderCommandHandler(LocalizationService localization,IOrdersService ordersService,ICartService cartService
            ,ICartProductService cartProductService,IProductService productService,IMapper mapper) : base(localization) 
        {
            this.localization = localization;
            this.ordersService = ordersService;
            this.cartService = cartService;
            this.cartProductService = cartProductService;
            this.productService = productService;
            this.mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateOrderModel request, CancellationToken cancellationToken)
        {
            // Check on Cart  
            var Cart =  cartService.GetCartByIdWithInclude(request.CartId);
            if (Cart == null) return BadRequest<int>(localization.Get("NotFound"));



            // list of product  that is in Cart
            var ProductList = Cart.CartProducts.Select(x => x.Product).ToList();

            var order = mapper.Map<Tradify.Data.Entities.Orders>(request);
            if (order != null)
            {
                foreach(var item in ProductList)
                {
                    if(item.InStock == true)
                    {
                      order.products.Add(item);
                    }
                     
                }

            }
             var result = await ordersService.AddAsync(order);
            if (result != null)
            {
                await ordersService.SaveChangesAsync();
                return Success(result.Id);
            }
            return BadRequest<int>(localization.Get("TryAgainInAnotherTime"));





        }
    }
}

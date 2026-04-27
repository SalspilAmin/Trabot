using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Cart.Commands.Models;
using Tradify.Core.Features.Cart.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Infrastructure.Context;
using Tradify.Service.AbstractsServices;
using Tradify.Service.Services;

namespace Tradify.Core.Features.Cart.Commands.Handlers
{
    public class CartCommandHandler : ResponseHandler, IRequestHandler<UpdateCartCommand, Response<GetCartByUserIdQueryResult>>
        , IRequestHandler<AddToCartCommand, Response<string>>
    {
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly UserManager<Tradify.Data.Entities.Identity.User> userManager;
        private readonly ICartService cartService;
        private readonly IProductVariantService productVariantService;
        private readonly ApplicationDbContext context;
        public CartCommandHandler(LocalizationService localization, IMapper mapper, UserManager<Tradify.Data.Entities.Identity.User> userManager
            , ICartService cartService, IProductVariantService productVariantService, ApplicationDbContext context) : base(localization)
        {
            this.localization = localization;
            this.mapper = mapper;
            this.userManager = userManager;
            this.cartService = cartService;
            this.context = context;
            this.productVariantService = productVariantService;
        }

        public async Task<Response<GetCartByUserIdQueryResult>> Handle(UpdateCartCommand request, CancellationToken cancellationToken)
        {
            using (var trans = await context.Database.BeginTransactionAsync())
            {

                try
                {
                    //Check User
                    //GeT user and Check
                    var user = await userManager.FindByIdAsync(request.UserId.ToString());
                    if (user == null) return BadRequest<GetCartByUserIdQueryResult>(localization.Get("NotFound"));
                    var Cart = user.Cart;

                    if (request.ProductsInCart.Count == 0)
                    {
                        Cart.CartProducts = null;


                    }
                    var productsinCartToUser = mapper.Map<List<CartProduct>>(request.ProductsInCart);
                    foreach (var item in productsinCartToUser)
                    {
                        item.Cart = Cart;
                        item.CartId = Cart.Id;
                    }
                    user.Cart.CartProducts = productsinCartToUser;
                    await context.SaveChangesAsync();
                    var result = mapper.Map<GetCartByUserIdQueryResult>(Cart);

                    return Success<GetCartByUserIdQueryResult>(result);
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return BadRequest<GetCartByUserIdQueryResult>(localization.Get("UnprocessableEntity"));

                }




            }

        }

        public async Task<Response<string>> Handle(AddToCartCommand request, CancellationToken cancellationToken)
        {
            using (var trans = await context.Database.BeginTransactionAsync())
            {

                try
                {

                    //GeT user and Check
                    var cart = await cartService.GetByIdAsync(request.CartId);
                    var productVariant = await productVariantService.GetByIdAsync(request.ProductVariant.Id);

                    if (cart == null || cart.IsDeleted || productVariant.IsDeleted || productVariant == null)
                        return BadRequest<string>(localization.Get("NotFound"));

                    var cartProduct = new CartProduct() { Cart = cart, CartId = cart.Id, ProductVariant = productVariant, ProductVariantId = productVariant.Id };
                    cart.CartProducts.Add(cartProduct);

                    await context.SaveChangesAsync();


                    return Success<string>(localization.Get("Success"));
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    return BadRequest<string>(localization.Get("UnprocessableEntity"));

                }
            }
        }
    }
}

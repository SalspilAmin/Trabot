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
using static Tradify.Data.AppMetaData.Router;

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

                    // If client sends empty list => clear cart
                    if (request.ProductsInCart == null || !request.ProductsInCart.Any())
                    {
                        context.CartProducts.RemoveRange(Cart.CartProducts);

                        await context.SaveChangesAsync();
                        await trans.CommitAsync();

                        var emptyResult = mapper.Map<GetCartByUserIdQueryResult>(Cart);

                        return Success(emptyResult);
                    }

                    // Existing items in DB

                    var existingCartProducts = Cart.CartProducts.ToList();

                    //  Use ProductVariantId as the stable identifier from the client
                    var requestedVariantIds = request.ProductsInCart
                        .Select(x => x.ProductVariantId)
                        .ToHashSet();

                    // Remove items no longer in the request
                    var productsToRemove = existingCartProducts
                        .Where(x => !requestedVariantIds.Contains((int)x.ProductVariantId))
                        .ToList();

                    context.CartProducts.RemoveRange(productsToRemove);
                    // Update or Add
                    foreach (var item in request.ProductsInCart)
                    {
                        // Update existing
                        if (item.Id != 0)
                        {
                            var existingProduct = existingCartProducts
                                .FirstOrDefault(x => x.Id == item.Id);

                            if (existingProduct != null)
                            {
                                existingProduct.ProductVariantId = item.ProductVariantId;
                                existingProduct.Quantity = item.Quantity;
                            }
                        }
                        else
                        {
                            // Add new product
                            var newCartProduct = new CartProduct
                            {
                                ProductVariantId = item.ProductVariantId,
                                Quantity = item.Quantity,
                                CartId = Cart.Id
                            };

                            await context.CartProducts.AddAsync(newCartProduct);
                        }
                    }

                    await context.SaveChangesAsync();
                    await trans.CommitAsync();

                    var result = mapper.Map<GetCartByUserIdQueryResult>(Cart);

                    return Success(result);
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

                    //GeT cart and  user and Check
                    var user = await userManager.FindByIdAsync(request.UserId.ToString());
                    var cart = await cartService.GetByIdAsync(request.CartId);
             //       if(cart.Id!=user.CartId) return BadRequest<string>(localization.Get("NotFound"));
                    var productVariant = await productVariantService.GetByIdAsync(request.ProductVariant.Id);

                    if (cart == null || cart.IsDeleted || productVariant == null || productVariant.IsDeleted)
                        return BadRequest<string>(localization.Get("NotFound"));

                    var cartProduct = new CartProduct() { Cart = cart, CartId = cart.Id, ProductVariant = productVariant, ProductVariantId = productVariant.Id,Quantity=request.ProductVariant.Quentity };
                    cart.CartProducts.Add(cartProduct);
                    

                    await context.SaveChangesAsync();

                    await trans.CommitAsync();
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

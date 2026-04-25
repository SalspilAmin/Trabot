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
    public class CartCommandHandler : ResponseHandler, IRequestHandler<UpdateCartCommand,Response<GetCartByUserIdQueryResult>>
    {
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly UserManager<Tradify.Data.Entities.Identity.User> userManager;
        private readonly ICartService cartService;
        private readonly ApplicationDbContext context;
        public CartCommandHandler(LocalizationService localization,IMapper mapper, UserManager<Tradify.Data.Entities.Identity.User> userManager
            ,ICartService cartService,ApplicationDbContext context) : base(localization)
        {
            this.localization = localization;
            this.mapper = mapper;
            this.userManager = userManager;
            this.cartService = cartService;
            this.context = context;
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
                catch (Exception ex) { 
                trans.Rollback();
                    return BadRequest<GetCartByUserIdQueryResult>(localization.Get("UnprocessableEntity"));

                }




            }

        }
    }
}

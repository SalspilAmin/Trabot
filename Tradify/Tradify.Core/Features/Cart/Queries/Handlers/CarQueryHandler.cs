using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Cart.Queries.Models;
using Tradify.Core.Features.Cart.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities.Identity;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.IdentityServices;

namespace Tradify.Core.Features.Cart.Queries.Handlers
{
    public class CarQueryHandler : ResponseHandler, IRequestHandler<GetCartByUserIdQuery, Response<GetCartByUserIdQueryResult>>
    {

        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly UserManager<Tradify.Data.Entities.Identity.User> userManager;
        




        #endregion

        #region Constructor

        public CarQueryHandler(LocalizationService localization,
             IMapper mapper, UserManager<Tradify.Data.Entities.Identity.User> userManager
            , ICateroriesService cateroriesService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.userManager = userManager;
          

        }

        #endregion
        public async Task<Response<GetCartByUserIdQueryResult>> Handle(GetCartByUserIdQuery request, CancellationToken cancellationToken)
        {
            //GeT user and Check
            var user = await userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return BadRequest<GetCartByUserIdQueryResult>(localization.Get("NotFound"));
            var Cart= user.Cart;
            var ProductsInCart = Cart.CartProducts;
            var result =  mapper.Map<GetCartByUserIdQueryResult>(Cart);

            return Success<GetCartByUserIdQueryResult>(result);
          

        }
    }
}

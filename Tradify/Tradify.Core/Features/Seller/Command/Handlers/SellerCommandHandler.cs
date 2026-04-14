using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Seller.Command.Models;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.IdentityServices;
using Tradify.Service.Services.IdentityServices;

namespace Tradify.Core.Features.Seller.Command.Handlers
{
    public class SellerCommandHandler : ResponseHandler, IRequestHandler<AddSellerCommand, Response<string>>
    {

    #region Fildes
        private readonly LocalizationService localize;
        private readonly ISellerService sellerService ;
        private readonly IMapper mapper;

    #endregion

    #region constructor

    public SellerCommandHandler(LocalizationService localization, ISellerService sellerService,
        IMapper mapper) : base(localization)
    {
        this.localize = localization;
        this.sellerService = sellerService;
        this.mapper = mapper;

    }


    #endregion

    #region Methods

    public async Task<Response<string>> Handle(AddSellerCommand request, CancellationToken cancellationToken)
    {
        var seller = mapper.Map<Sellers>(request);

            var result = await sellerService.AddSellerAsync(seller);
        switch (result.Item1)
        {

            case "UserNotFound":
                return BadRequest<string>(localize.Get("UserNotFound"));
                break;
            case "UserDeleted":
                return BadRequest<string>(localize.Get("UserDeleted"));
                break;
            case "UserIsNotAssignedto(Seller_Role)":
                return BadRequest<string>(localize.Get("UserIsNotAssignedto(Seller_Role)"));
                break;
            case "UserIsAlreadySeller":
                return BadRequest<string>(localize.Get("UserIsAlreadySeller"));

                break;

                case "BusinessNameAlreadyExist":
                return BadRequest<string>(localize.Get("BusinessNameAlreadyExist"));
               
                    break;
                    
                case "Failed":
                    return BadRequest<string>(localize.Get("Failed"));
                    break;
                case "Success":
                return Success<string>(result.Item1, meta: result.Item2);
                break;
               default: return BadRequest<string>(result.Item1);
        }

    }

   

    #endregion
}
}

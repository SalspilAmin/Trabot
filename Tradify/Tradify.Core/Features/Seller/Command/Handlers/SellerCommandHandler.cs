using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Instructor.Command.Models;
using Tradify.Core.Features.Seller.Command.Models;
using Tradify.Core.Features.Seller.Queries.Results;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities;
using Tradify.Data.Enums;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.AuthorizationServices;
using Tradify.Service.AbstractsServices.IdentityServices;
using Tradify.Service.Services;
using Tradify.Service.Services.IdentityServices;

namespace Tradify.Core.Features.Seller.Command.Handlers
{
    public class SellerCommandHandler : ResponseHandler
                                                      , IRequestHandler<AddSellerCommand, Response<string>>
                                                      , IRequestHandler<UpdateSellerCommand, Response<string>>
                                                      , IRequestHandler<ActiveSellerCommand, Response<string>>
                                                      , IRequestHandler<DisActiveSellerCommand, Response<string>>



    {

        #region Fildes
        private readonly LocalizationService localize;
        private readonly ISellerService sellerService ;
        private readonly IMapper mapper;
        private readonly IAuthorizationService authorizationService ;
        private readonly IUserService userService;
        private readonly UserManager<Data.Entities.Identity.User> userManager;
        private readonly ICurrentUserService currentUserService;

        #endregion

        #region constructor

        public SellerCommandHandler(LocalizationService localization
            , ISellerService sellerService,
        IMapper mapper,IAuthorizationService authorizationService
            , UserManager<Data.Entities.Identity.User> userManager
            , IUserService userService
            , ICurrentUserService currentUserService
            ) : base(localization)
        {
            this.localize = localization;
            this.sellerService = sellerService;
            this.mapper = mapper;
            this.authorizationService = authorizationService;
            this.userService = userService;
            this.userManager = userManager;
            this.currentUserService = currentUserService;
        }


        #endregion

        #region Methods

        public async Task<Response<string>> Handle(AddSellerCommand request, CancellationToken cancellationToken)
    {
            var user = mapper.Map<Data.Entities.Identity.User>(request);

            var userResult = await userService.AddUserAsync(user, request.Password);

            switch (userResult.Item1)
            {

                case "EmailOrPhoneIsExist":
                    return BadRequest<string>(localize.Get("EmailOrPhoneIsExist"));
                    break;
                case "UserNameIsExist":
                    return BadRequest<string>(localize.Get("UserNameIsExist"));
                    break;
                case "Add_Correct_info":
                    return BadRequest<string>(localize.Get("Add_Correct_info"));
                    break;
                case "Failed":
                    return BadRequest<string>(localize.Get("TryToRegisterAgain"));

                    break;

                case "Success":
                    break;

                default:
                    return BadRequest<string>(userResult.Item1);
            }

            var userId = userResult.Item2.Value;

            var seller = mapper.Map<Sellers>(request);

            var result = await sellerService.AddSellerAsync(seller, userId);
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



        // Update Seller  

        public async Task<Response<string>> Handle(UpdateSellerCommand request, CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();
            var user = userManager.Users.IgnoreQueryFilters()
                .FirstOrDefault(x => x.Id == userId);

            if (user == null)
                return NotFound<string>(localize.Get("UserNotFound"));


            if (user.IsDeleted)
                return NotFound<string>(localize.Get("SellerLinkedToDeletedUser"));


            var seller = await sellerService.GetTableAsTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (seller == null)
                return NotFound<string>(localize.Get("SellerNotFound"));

            if (!seller.IsActive)
                return NotFound<string>(localize.Get("SellerNotActive"));



            seller.BusinessType = request.BusinessType;
            seller.BusinessName = request.BusinessName;
           
            
            await sellerService.SaveChangesAsync();

            return Success<string>(localize.Get("SellerUpdatedSuccessfully"));
        }


        // Dis Active Seller  

        public async Task<Response<string>> Handle(DisActiveSellerCommand request, CancellationToken cancellationToken)
        {
            
            var seller = await sellerService.GetTableAsTracking()
                .FirstOrDefaultAsync(i => i.Id == request.Id);

            if (seller == null)
                return BadRequest<string>(localize.Get("SellerNotFound"));

            if (!seller.IsActive)
                return BadRequest<string>(localize.Get("SellerIsAlreadyNotActive"));


            seller.IsActive = false;


            await sellerService.SaveChangesAsync();

            return Success<string>(localize.Get("SellerDisActiveSuccessfully"));
        }

        // Active Seller  

        public async Task<Response<string>> Handle(ActiveSellerCommand request, CancellationToken cancellationToken)
        {
            

            var seller = await sellerService.GetTableAsTracking().IgnoreQueryFilters()
                .FirstOrDefaultAsync(i => i.Id == request.Id);

            if (seller == null)
                return BadRequest<string>(localize.Get("SellerNotFound"));

            if (seller.IsActive)
                return BadRequest<string>(localize.Get("SellerIsAlreadyActive"));


            seller.IsActive = true;


            await sellerService.SaveChangesAsync();

            return Success<string>(localize.Get("SellerActiveSuccessfully"));
        }


        #endregion
    }
}

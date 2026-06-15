using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Seller.Queries.Models;
using Tradify.Core.Features.Seller.Queries.Results;
using Tradify.Core.Features.Store.Queries.Models;
using Tradify.Core.Features.Store.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.IdentityServices;
using Tradify.Service.Services;
using Twilio.TwiML.Voice;

namespace Tradify.Core.Features.Seller.Queries.Handlers
{
    public class SellerQueryHandler : ResponseHandler, IRequestHandler<GetSellerByIdQuery, Response<GetSellerByIdResponse>>
                                                     , IRequestHandler<GetAllSellerQuery, PaginatedResult<GetAllSellerResponse>>
                                                     , IRequestHandler<GetSellerProfileQuery, Response<GetSellerByIdResponse>>
                                                     , IRequestHandler<GetSellerNotHaveStoreQuery, PaginatedResult<GetAllSellerResponse>>



    {
        #region fields
        private readonly IMapper mapper;
        private readonly LocalizationService localization;
        private readonly IStoreService storeService;
        private readonly IFileService fileService;
        private readonly ISellerService sellerService;
        private readonly ICurrentUserService currentUserService;
        private readonly IUserService userService;
        private readonly UserManager<Data.Entities.Identity.User> userManager;
        #endregion

        #region Constructor
        public SellerQueryHandler(LocalizationService localization, IFileService fileService
            , IMapper mapper, IStoreService storeService
            ,ISellerService sellerService
            ,ICurrentUserService currentUserService
             , UserManager<Data.Entities.Identity.User> userManager
            , IUserService userService) : base(localization)
        {
            this.mapper = mapper;
            this.localization = localization;
            this.storeService = storeService;
            this.fileService = fileService;
            this.sellerService= sellerService;  
            this.currentUserService= currentUserService;
            this.userService = userService;
            this.userManager = userManager;
        }

        #endregion

        #region Mehtods
        // Get All Seller Pagination
        public async Task<PaginatedResult<GetAllSellerResponse>> Handle(GetAllSellerQuery request, CancellationToken cancellationToken)
        {

            var sellers = sellerService.GetTableNoTracking().AsQueryable();

            if (request.IsActive.HasValue)
            {
                sellers = sellers.IgnoreQueryFilters()
                                   .Where(v => v.IsActive == request.IsActive);
            }

            var result = await mapper
                .ProjectTo<GetAllSellerResponse>(sellers)
                .ToPaginationlist(request.PageNumber, request.PageSize);

            return result;
        }


        public async Task<PaginatedResult<GetAllSellerResponse>> Handle(GetSellerNotHaveStoreQuery request, CancellationToken cancellationToken)
        {


            var sellers = sellerService.GetTableNoTracking()
    .Where(s => s.Store == null)
    .AsQueryable();

            if (request.IsActive.HasValue)
            {
                sellers = sellers.IgnoreQueryFilters()
                                   .Where(v => v.IsActive == request.IsActive);
            }

            var result = await mapper
                .ProjectTo<GetAllSellerResponse>(sellers)
                .ToPaginationlist(request.PageNumber, request.PageSize);

            return result;
        }


        // Get seller By Id
        public async Task<Response<GetSellerByIdResponse>> Handle(GetSellerByIdQuery request, CancellationToken cancellationToken)
        {
            var seller = await sellerService.GetTableNoTracking()
                .Include(x => x.User)
                .Include(x => x.Store)
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(p => p.Id == request.Id);


            if (seller == null) 
                return NotFound<GetSellerByIdResponse>(localization.Get("SellerNotFound"));

            
            var result = mapper.Map<GetSellerByIdResponse>(seller);


            return Success<GetSellerByIdResponse>(result);

        }



        // Get seller By Token
        public async Task<Response<GetSellerByIdResponse>> Handle(GetSellerProfileQuery request, CancellationToken cancellationToken)
        {
            var userId = currentUserService.GetUserId();
            var user = userManager.Users.IgnoreQueryFilters()
                .FirstOrDefault(x => x.Id == userId);

            if (user == null)
                return NotFound<GetSellerByIdResponse>(localization.Get("UserNotFound"));


            if (user.IsDeleted)
                return NotFound<GetSellerByIdResponse>(localization.Get("SellerLinkedToDeletedUser"));


            var seller = await sellerService.GetTableNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (seller == null)
                return NotFound<GetSellerByIdResponse>(localization.Get("SellerNotFound"));

            if (!seller.IsActive)
            return NotFound<GetSellerByIdResponse>(localization.Get("SellerNotActive"));

            var result = mapper.Map<GetSellerByIdResponse>(seller);


            return Success<GetSellerByIdResponse>(result);

        }

        #endregion
    }


}


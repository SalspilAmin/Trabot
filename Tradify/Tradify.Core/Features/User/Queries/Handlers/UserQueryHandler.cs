using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.User.Queries.Models;
using Tradify.Core.Features.User.Queries.Results;
using Tradify.Core.Resources.Service;
using Tradify.Core.Wrappers;
using Tradify.Data.Helpers.Results;
using Tradify.Service.AbstractsServices.AuthenticationServices;
using Tradify.Service.AbstractsServices.IdentityServices;

namespace Tradify.Core.Features.User.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler,IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
        ,IRequestHandler<GetUserPaginationQuery, PaginatedResult<GetUserPaginationReponse>>,
        IRequestHandler<GetUserByTokenQuery, Response<UserInfoFromToken>>
    {
        #region fields
        private readonly IMapper mapper;
        private readonly UserManager<Tradify.Data.Entities.Identity.User> _userManager;
        private readonly LocalizationService localization;
        private readonly IUserService userService;
        
        #endregion

        #region Constructor
        public UserQueryHandler(LocalizationService localization, UserManager<Tradify.Data.Entities.Identity.User> userManager
            ,IMapper mapper,IUserService userService) : base(localization)
        {
            this._userManager = userManager;
            this.mapper = mapper;
            this.localization = localization;
            this.userService = userService; 
            
        }

       



        #endregion

        #region Mehtods
        public async Task<Response<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var User =  _userManager.Users.Where(x=>x.IsDeleted==false).FirstOrDefault(x=>x.Id==request.Id);
            if (User == null) return NotFound<GetUserByIdResponse>(localization.Get("NotFound"));
        
            var result =  mapper.Map<GetUserByIdResponse>(User);

            return Success<GetUserByIdResponse>(result);
           
        }

        public async Task<PaginatedResult<GetUserPaginationReponse>> Handle(GetUserPaginationQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.Where(x => x.IsDeleted == false);
            var result = await mapper.ProjectTo<GetUserPaginationReponse>(users).ToPaginationlist(request.PageNumber, request.PageSize);

            return result;
        }

        public async Task<Response<UserInfoFromToken>> Handle(GetUserByTokenQuery request, CancellationToken cancellationToken)
        {
            var result = await userService.GetUserInformationByToken(request.AccessToken);
            if (result == null) return NotFound<UserInfoFromToken>(localization.Get("NotFound"));
            var user = await _userManager.FindByIdAsync(result.UserId);
            result.CartId = user.CartId;
            return Success<UserInfoFromToken>(result);      
        }
        #endregion

    }
}

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

namespace Tradify.Core.Features.User.Queries.Handlers
{
    public class UserQueryHandler : ResponseHandler,IRequestHandler<GetUserByIdQuery, Response<GetUserByIdResponse>>
        ,IRequestHandler<GetUserPaginationQuery, PaginatedResult<GetUserPaginationReponse>>
    {
        #region fields
        private readonly IMapper mapper;
        private readonly UserManager<Tradify.Data.Entities.Identity.User> _userManager;
        private readonly LocalizationService localization;
        #endregion

        #region Constructor
        public UserQueryHandler(LocalizationService localization, UserManager<Tradify.Data.Entities.Identity.User> userManager
            ,IMapper mapper) : base(localization)
        {
            this._userManager = userManager;
            this.mapper = mapper;
            this.localization = localization;
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
        #endregion

    }
}

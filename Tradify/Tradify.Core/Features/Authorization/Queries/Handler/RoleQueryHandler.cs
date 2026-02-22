using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Authorization.Queries.Models;
using Tradify.Core.Features.Authorization.Queries.Results;
using Tradify.Core.Resources;
using Tradify.Core.Resources.Service;
using Tradify.Data.Helpers.Results;
using Tradify.Service.AbstractsServices.AuthorizationServices;

namespace Tradify.Core.Features.Authorization.Queries.Handler
{
    public  class RoleQueryHandler : ResponseHandler,
       IRequestHandler<GetRolesListQuery, Response<List<GetRolesListResult>>>,
       IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdResult>>
        ,IRequestHandler<ManageUserRolesQuery,Response<ManageUserRolesResult>>
    {


        #region Fields
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;
        private readonly LocalizationService _stringLocalizer;
        private readonly UserManager<Tradify.Data.Entities.Identity.User> _userManager;
        #endregion
        #region Constructors
        public RoleQueryHandler(LocalizationService stringLocalizer,
                                IAuthorizationService authorizationService,
                                IMapper mapper,
                                UserManager<Tradify.Data.Entities.Identity.User> userManager) : base(stringLocalizer)
        {
            _authorizationService = authorizationService;
            _mapper = mapper;
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<List<GetRolesListResult>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetRolesList();
            var result = _mapper.Map<List<GetRolesListResult>>(roles);
            return Success(result);
        }

        public async Task<Response<GetRoleByIdResult>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRoleById(request.Id);
            if (role == null) return NotFound<GetRoleByIdResult>(_stringLocalizer.Get("RoleNotExist"));
            var result = _mapper.Map<GetRoleByIdResult>(role);
            return Success(result);
        }

        public async Task<Response<ManageUserRolesResult>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return NotFound<ManageUserRolesResult>(_stringLocalizer.Get("UserIsNotFound"));
            var result = await _authorizationService.ManageUserRolesData(user);
            return Success(result);
        }


        #endregion
    }
}

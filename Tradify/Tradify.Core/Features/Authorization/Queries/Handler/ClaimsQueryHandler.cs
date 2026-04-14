using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Authorization.Queries.Models;
using Tradify.Core.Resources;
using Tradify.Core.Resources.Service;
using Tradify.Data.Helpers.Results;
using Tradify.Service.AbstractsServices.AuthorizationServices;

namespace Tradify.Core.Features.Authorization.Queries.Handler
{
    public class ClaimsQueryHandler : ResponseHandler,
        IRequestHandler<ManageUserClaimsQuery, Response<ManageUserClaimsResult>>
    {

        #region Fileds
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<Tradify.Data.Entities.Identity.User> _userManager;
        private readonly LocalizationService _stringLocalizer;
        #endregion
        #region Constructors
        public ClaimsQueryHandler(LocalizationService stringLocalizer,
                                  IAuthorizationService authorizationService,
                                  UserManager<Tradify.Data.Entities.Identity.User> userManager) : base(stringLocalizer)
        {
            _authorizationService = authorizationService;
            _userManager = userManager;
            _stringLocalizer = stringLocalizer;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<ManageUserClaimsResult>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
        {
            var user = _userManager.Users.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Id == request.UserId);
            if (user == null) return NotFound<ManageUserClaimsResult>(_stringLocalizer.Get("UserIsNotFound"));
            var result = await _authorizationService.ManageUserClaimData(user);
            return Success(result);
        }
        #endregion
    }
}

using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Authorization.Commands.Models;
using Tradify.Core.Resources;
using Tradify.Core.Resources.Service;
using Tradify.Service.AbstractsServices.AuthorizationServices;

namespace Tradify.Core.Features.Authorization.Commands.Handler
{
    public class ClaimsCommandHandler : ResponseHandler,
         IRequestHandler<UpdateUserClaimsCommand, Response<string>>
    {
        #region Fileds
        private readonly LocalizationService _stringLocalizer;
        private readonly IAuthorizationService _authorizationService;

        #endregion
        #region Constructors
        public ClaimsCommandHandler(LocalizationService stringLocalizer,
                                    IAuthorizationService authorizationService) : base(stringLocalizer)
        {
            _authorizationService = authorizationService;
            _stringLocalizer = stringLocalizer;
        }
        #endregion
        #region Handle Functions
        public async Task<Response<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.UpdateUserClaims(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(_stringLocalizer.Get("UserIsNotFound"));
                case "FailedToRemoveOldClaims": return BadRequest<string>(_stringLocalizer.Get("FailedToRemoveOldClaims"));
                case "FailedToAddNewClaims": return BadRequest<string>(_stringLocalizer.Get("FailedToAddNewClaims"));
                case "FailedToUpdateClaims": return BadRequest<string>(_stringLocalizer.Get("FailedToUpdateClaims"));
            }
            return Success<string>(_stringLocalizer.Get("Success"));
        }
        #endregion
    }
}

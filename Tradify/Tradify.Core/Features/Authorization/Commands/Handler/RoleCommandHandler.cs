using MediatR;
using Microsoft.AspNetCore.Identity;
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
    public class RoleCommandHandler : ResponseHandler, IRequestHandler<AddRoleCommand, Response<string>>
        ,IRequestHandler<DeleteRoleCommand, Response<string>>
        ,IRequestHandler<EditRoleCommand, Response<string>>
        , IRequestHandler<UpdateUserRolesCommand, Response<string>>
    {
        private readonly LocalizationService localization;
        private readonly IAuthorizationService authorizationService;
        public RoleCommandHandler(LocalizationService localization,IAuthorizationService authorizationService) : base(localization)
        {
            this.localization = localization;
            this.authorizationService = authorizationService;
        }

        public async Task<Response<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {
            var check = await authorizationService.IsRoleExistByName(request.RoleName);
            if(check==true) return BadRequest<string>(localization.Get("IsExist"));
            var result = await authorizationService.AddRoleAsync(request.RoleName);
            if (result == "Success") return Success("Success");
            return BadRequest<string>(localization.Get("AddFailed"));
        }

        public async Task<Response<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {


            var result = await authorizationService.EditRoleAsync(request);
            if (result == "notFound") return NotFound<string>();
            else if (result == "Success") return Success(localization.Get("Updated"));
            else
                return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await authorizationService.DeleteRoleAsync(request.Id);
            if (result == "NotFound") return NotFound<string>();
            else if (result == "Used") return BadRequest<string>(localization.Get("RoleIsUsed"));
            else if (result == "Success") return Success(localization.Get("Deleted"));
            else
                return BadRequest<string>(result);
        }

        public async Task<Response<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
        {
            var result = await authorizationService.UpdateUserRoles(request);
            switch (result)
            {
                case "UserIsNull": return NotFound<string>(localization.Get("UserIsNotFound"));
                case "FailedToRemoveOldRoles": return BadRequest<string>(localization.Get("FailedToRemoveOldRoles"));
                case "FailedToAddNewRoles": return BadRequest<string>(localization.Get("FailedToAddNewRoles"));
                case "FailedToUpdateUserRoles": return BadRequest<string>(localization.Get("FailedToUpdateUserRoles"));
            }
            return Success<string>(localization.Get("Success"));
        }
    }
}

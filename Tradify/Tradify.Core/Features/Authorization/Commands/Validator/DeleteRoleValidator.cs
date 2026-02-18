using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Authorization.Commands.Models;
using Tradify.Core.Resources;
using Tradify.Core.Resources.Service;
using Tradify.Service.AbstractsServices.AuthorizationServices;

namespace Tradify.Core.Features.Authorization.Commands.Validator
{
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
    {
        #region Fields
        private readonly LocalizationService _stringLocalizer;
        public readonly IAuthorizationService _authorizationService;

        #endregion
        #region Constructors
        public DeleteRoleValidator(LocalizationService stringLocalizer, IAuthorizationService authorizationService)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }
        #endregion
        #region  Functions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage(_stringLocalizer.Get("NotEmpty"))
                 .NotNull().WithMessage(_stringLocalizer.Get("Required"));
        }
        public void ApplyCustomValidationsRules()
        {
            //RuleFor(x => x.Id)
            //    .MustAsync(async (Key, CancellationToken) => await _authorizationService.IsRoleExistById(Key))
            //    .WithMessage(_stringLocalizer[SharedResourcesKeys.RoleNotExist]);
        }
        #endregion


    }
}

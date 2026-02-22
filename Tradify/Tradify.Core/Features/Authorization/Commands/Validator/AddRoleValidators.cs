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
    public class AddRoleValidators : AbstractValidator<AddRoleCommand>
    {
        #region Fields
        private readonly LocalizationService localization;
        private readonly IAuthorizationService _authorizationService;
        #endregion
        #region Constructors

        #endregion
        public AddRoleValidators(  LocalizationService localization,
                                 IAuthorizationService authorizationService)
        {
            localization = localization;
            _authorizationService = authorizationService;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.RoleName)
                 .NotEmpty().WithMessage(localization.Get("NotEmpty"))
                 .NotNull().WithMessage(localization.Get("Required"));
        }

        public void ApplyCustomValidationsRules()
        {
            RuleFor(x => x.RoleName)
                .MustAsync(async (Key, CancellationToken) => !await _authorizationService.IsRoleExistByName(Key))
                .WithMessage(localization.Get("IsExist"));
        }

        #endregion
    }
}

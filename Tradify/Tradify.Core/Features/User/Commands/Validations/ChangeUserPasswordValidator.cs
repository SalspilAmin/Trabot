using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Core.Resources;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.User.Commands.Validations
{
    public class ChangeUserPasswordValidator : AbstractValidator<ChangeUserPasswordCommand>
    {
        #region Fields
        private readonly LocalizationService localization;
        #endregion
        #region Constructor
        public ChangeUserPasswordValidator(LocalizationService localization)
        {
            this.localization = localization;
            ApplyValidationsRules();
        }
        #endregion
        #region validations

        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(localization.Get("NotEmpty"))
                .NotNull().WithMessage(localization.Get("Required"));
            RuleFor(x=>x.CurrentPassword)
                .NotEmpty().WithMessage(localization.Get("NotEmpty"))
                .NotNull().WithMessage(localization.Get("Required"));
            RuleFor(x => x.NewPassword)
                  .NotEmpty().WithMessage(localization.Get("NotEmpty"))
                .NotNull().WithMessage(localization.Get("Required"));
            RuleFor(x=>x.ConfirmPassword).Equal(x=>x.NewPassword).WithMessage(localization.Get("PasswordNotEqualConfirmPass"))
                 .NotEmpty().WithMessage(localization.Get("NotEmpty"))
                .NotNull().WithMessage(localization.Get("Required"));

        }
        #endregion

    }
}

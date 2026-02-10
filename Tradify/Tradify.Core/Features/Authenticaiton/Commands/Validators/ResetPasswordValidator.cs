using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Authenticaiton.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Authenticaiton.Commands.Validators
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {

        private readonly LocalizationService localization;

        public ResetPasswordValidator(LocalizationService localization) 
        {
        this.localization = localization;

        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.EmailOrPhone)
                 .NotEmpty().WithMessage(localization.Get("NotEmpty"))
                 .NotNull().WithMessage(localization.Get("Required"));

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(localization.Get("NotEmpty"))
                .NotNull().WithMessage(localization.Get("Required"));

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage(localization.Get("NotEmpty"))
                .NotNull().WithMessage(localization.Get("Required"))
                .Equal(x=>x.Password).WithMessage(localization.Get("PasswordNotEqualConfirmPass"));

        }
    }
}

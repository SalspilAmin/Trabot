using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Authenticaiton.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Authenticaiton.Commands.Validators
{
    public class ConfirmResetPasswordValidator : AbstractValidator<ConfirmResetPasswordCommand>
    {
        private readonly LocalizationService localization;
        public ConfirmResetPasswordValidator(LocalizationService localization) 
        {
        
           this.localization = localization;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.EmailOrPhone)
                 .NotEmpty().WithMessage(localization.Get("NotEmpty"))
                 .NotNull().WithMessage(localization.Get("Required"));
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage(localization.Get("NotEmpty"))
                .NotNull().WithMessage(localization.Get("Required"));
        }

    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Authenticaiton.Queries.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Authenticaiton.Queries.Validators
{
    public class ConfirmPhoneValidator : AbstractValidator<ConfirmPhoneQuery>
    {
        private readonly LocalizationService localizationService;
        public ConfirmPhoneValidator(LocalizationService localizationService) 
        {
            this.localizationService = localizationService;
        
        }

        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage(localizationService.Get("NotEmpty"))
                 .NotNull().WithMessage(localizationService.Get("Required"));

            RuleFor(x => x.OTP)
                .NotEmpty().WithMessage(localizationService.Get("NotEmpty"))
                .NotNull().WithMessage(localizationService.Get("Required"));
        }

    }
}

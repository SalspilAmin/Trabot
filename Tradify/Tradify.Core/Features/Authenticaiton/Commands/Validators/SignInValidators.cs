using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Authenticaiton.Commands.Models;
using Tradify.Core.Resources;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Authenticaiton.Commands.Validators
{
    public class SignInValidators : AbstractValidator<SignInCommand>
    {

        #region Fields
        private readonly LocalizationService localization;

        #endregion
        #region Constructor
        public SignInValidators(LocalizationService localization)
        {
            this.localization = localization;
            ApplyValidationsRules();
        }

        #endregion

        #region Methods
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.EmailOrPhone)
                 .NotEmpty().WithMessage(localization.Get("NotEmpty"))
                 .NotNull().WithMessage(localization.Get("Required" ));

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(localization.Get("NotEmpty"))
                .NotNull().WithMessage(localization.Get("Required"));
        }

        #endregion
    }
}

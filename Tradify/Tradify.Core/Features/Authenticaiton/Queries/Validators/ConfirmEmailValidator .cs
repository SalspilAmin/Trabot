using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Authenticaiton.Queries.Models;
using Tradify.Core.Resources;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Authenticaiton.Queries.Validators
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailQuery>
    {
        #region Fields
        private readonly LocalizationService _localizer;
        #endregion

        #region constructor
        public ConfirmEmailValidator(LocalizationService localizer)
        {
            _localizer = localizer;
            ApplyValidationsRules();
        }

        #endregion

        #region Validations
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage(_localizer.Get("NotEmpty"))
                 .NotNull().WithMessage(_localizer.Get("Required"));

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage(_localizer.Get("NotEmpty"))
                .NotNull().WithMessage(_localizer.Get("Required"));
        }

        #endregion

    }
}

using FluentValidation;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Authorization.Commands.Models;
using Tradify.Core.Resources;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Authorization.Commands.Validator
{
    public class EditRoleValidator : AbstractValidator<EditRoleCommand>
    {
        #region Fields
        private readonly LocalizationService _stringLocalizer;
        #endregion
        #region Constructors

        #endregion
        public EditRoleValidator(LocalizationService stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            ApplyValidationsRules();
            ApplyCustomValidationsRules();
        }

        #region Actions
        public void ApplyValidationsRules()
        {
            RuleFor(x => x.Id)
                 .NotEmpty().WithMessage(_stringLocalizer.Get("NotEmpty"))
                 .NotNull().WithMessage(_stringLocalizer.Get("Required"));

            RuleFor(x => x.Name)
                 .NotEmpty().WithMessage(_stringLocalizer.Get("NotEmpty"))
                 .NotNull().WithMessage(_stringLocalizer.Get("Required"));
        }

        public void ApplyCustomValidationsRules()
        {

        }

        #endregion
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Education.Command.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Education.Command.Validations
{
    public class UpdateInstructorEducationValidator : AbstractValidator<UpdateInstructorEducationCommand>
    {

        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public UpdateInstructorEducationValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductValidations();
        }


        #endregion

        #region Validations
        public void ApplyProductValidations()
        {

            RuleFor(x => x.Id)
               .GreaterThan(0).WithMessage(localize.Get("IdMustBeGreaterThanZero"))
               .NotEmpty().WithMessage(localize.Get("NotEmpty"))
               .NotNull().WithMessage(localize.Get("Required"));

            RuleFor(x => x.Degree)
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .MaximumLength(255).WithMessage(localize.Get("MaxLengthis255"));

            RuleFor(x => x.Institution)
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .MaximumLength(255).WithMessage(localize.Get("MaxLengthis255"));

            RuleFor(x => x.Year)
                .GreaterThanOrEqualTo(0).WithMessage(localize.Get("YearGreaterThanZero"))
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"));
        }
        #endregion
    }
}

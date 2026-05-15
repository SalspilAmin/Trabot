using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Education.Command.Models;
using Tradify.Core.Features.Instructor.Command.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Instructor.Command.Validations
{
    public class ActiveInstructorValidator : AbstractValidator<ActiveInstructorCommand>
    {

        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public ActiveInstructorValidator(LocalizationService localization)
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

        }
        #endregion
    }
}

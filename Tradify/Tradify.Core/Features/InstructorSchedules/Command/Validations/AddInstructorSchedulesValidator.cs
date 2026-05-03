using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Education.Command.Models;
using Tradify.Core.Features.Instructor.Command.Models;
using Tradify.Core.Features.InstructorSchedules.Command.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.InstructorSchedules.Command.Validations
{
    public class AddInstructorSchedulesValidator : AbstractValidator<AddInstructorSchedulesCommand>
    {

        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddInstructorSchedulesValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductValidations();
        }


        #endregion

        #region Validations
        public void ApplyProductValidations()
        {

            RuleFor(x => x.StartTime)
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"));

            RuleFor(x => x.Day)
               .NotNull().WithMessage(localize.Get("Required"))
               .NotEmpty().WithMessage(localize.Get("NotEmpty"));

            RuleFor(x => x.EndTime)
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"));

            RuleFor(x => x.Capacity)
                .GreaterThanOrEqualTo(0).WithMessage(localize.Get("CapacityMustGreaterThanZero"))
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"));
        }
        #endregion
    }
}

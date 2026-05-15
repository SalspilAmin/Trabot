using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.InstructorSchedules.Command.Models;
using Tradify.Core.Features.InstructorService.Command.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.InstructorSchedules.Command.Validations
{
    public class UpdateInstructorSchedulesValidator : AbstractValidator<UpdateInstructorSchedulesCommand>
    {

        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public UpdateInstructorSchedulesValidator(LocalizationService localization)
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

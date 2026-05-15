using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.InstructorSchedules.Command.Models;
using Tradify.Core.Features.InstructorService.Command.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.InstructorSchedules.Command.Validations
{
    public class DeleteInstructorSchedulesValidator : AbstractValidator<DeleteInstructorSchedulesCommand>
    {

        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public DeleteInstructorSchedulesValidator(LocalizationService localization)
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

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Certification.Command.Models;
using Tradify.Core.Features.InstructorService.Command.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.InstructorService.Command.Validations
{
    public class UpdateInstructorServiceValidator : AbstractValidator<UpdateInstructorServiceCommand>
    {

        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public UpdateInstructorServiceValidator(LocalizationService localization)
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

            RuleFor(x => x.Name)
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .MaximumLength(255).WithMessage(localize.Get("MaxLengthis255"));


        }
        #endregion
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Certification.Command.Models;
using Tradify.Core.Features.Education.Command.Models;
using Tradify.Core.Features.Instructor.Command.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Certification.Command.Validations
{
    public class AddInstructorCertificationValidator : AbstractValidator<AddInstructorCertificationCommand>
    {

        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddInstructorCertificationValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductValidations();
        }


        #endregion

        #region Validations
        public void ApplyProductValidations()
        {

           

            RuleFor(x => x.Title)
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .MaximumLength(1000).WithMessage(localize.Get("MaxLengthis1000"));


        }
        #endregion
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Discount.Instructor.Command.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Discount.Instructor.Command.Validations
{
    public class DeleteInstructorDiscountValidator : AbstractValidator<DeleteInstructorDiscountCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public DeleteInstructorDiscountValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductValidations();
        }


        #endregion

        #region Validations
        public void ApplyProductValidations()
        {


        }
        #endregion
    }
}

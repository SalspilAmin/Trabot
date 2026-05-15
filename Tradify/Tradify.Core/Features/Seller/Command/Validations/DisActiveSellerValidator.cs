using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Instructor.Command.Models;
using Tradify.Core.Features.Seller.Command.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Seller.Command.Validations
{
    public class DisActiveSellerValidator : AbstractValidator<DisActiveSellerCommand>
    {

        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public DisActiveSellerValidator(LocalizationService localization)
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

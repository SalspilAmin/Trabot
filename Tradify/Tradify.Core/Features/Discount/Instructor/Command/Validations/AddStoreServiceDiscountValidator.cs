using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Discount.Instructor.Command.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Discount.Instructor.Command.Validations
{
    public class AddStoreServiceDiscountValidator : AbstractValidator<AddStoreServiceDiscountCommand>
    {

        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddStoreServiceDiscountValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductValidations();
        }


        #endregion

        #region Validations
        public void ApplyProductValidations()
        {


            RuleFor(x => x.Discount)
                .InclusiveBetween(0, 100)
                .WithMessage(localize.Get("DiscountBetween0And100"));
        }

        #endregion
    }
}

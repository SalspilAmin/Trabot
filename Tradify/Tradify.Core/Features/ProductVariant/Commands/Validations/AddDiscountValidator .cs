using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.ProductVariant.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.ProductVariant.Commands.Validations
{
    public class AddDiscountValidator : AbstractValidator<AddDiscountCommand>
    {      
        
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddDiscountValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductValidations();
        }


        #endregion

        #region Validations
        public void ApplyProductValidations()
        {
            RuleFor(x => x.VariantId)
                .GreaterThan(0)
                .WithMessage(localize.Get("Required"));

            RuleFor(x => x.Discount)
                .InclusiveBetween(0, 100)
                .WithMessage(localize.Get("DiscountBetween0And100"));
        }

        #endregion
    }
}

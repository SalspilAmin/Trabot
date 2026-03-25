using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.ProductVariant.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.ProductVariant.Commands.Validations
{
    public class UpdateProductVariantValidator : AbstractValidator<UpdateProductVariantCommand>
    {
        #region Fields
        private readonly LocalizationService localize;
        #endregion

        #region Constructor
        public UpdateProductVariantValidator(LocalizationService localization)
        {
            localize = localization;
            ApplyValidations();
        }
        #endregion

        #region Validations
        private void ApplyValidations()
        {


            RuleFor(x => x.Id)
               .GreaterThan(0).WithMessage(localize.Get("Required"));

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage(localize.Get("PriceGreaterThanZero"));

            RuleFor(x => x.NumberOfProductInStock)
                .GreaterThanOrEqualTo(0).WithMessage(localize.Get("StockGreaterThanZero"));

            RuleFor(x => x.Discount)
                .InclusiveBetween(0, 100)
                .WithMessage(localize.Get("DiscountBetween0And100"));

        }
           
        #endregion
    }
}

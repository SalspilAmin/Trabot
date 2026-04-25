using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.ProductVariant.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.ProductVariant.Commands.Validations
{
    public class AddProductVariantValidator : AbstractValidator<AddProductVariantCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddProductVariantValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductVariantValidator();
        }


        #endregion
        public void ApplyProductVariantValidator()
        {
            



            RuleFor(x => x.Price).GreaterThan(0).WithMessage(localize.Get("PriceGreaterThanZero"));
            RuleFor(x => x.NumberOfProductInStock).GreaterThanOrEqualTo(0).WithMessage(localize.Get("StockGreaterThanZero"));

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .GreaterThan(0).WithMessage(localize.Get("ProductIdGreaterThanZero"));
            RuleFor(x => x.Discount)
                      .GreaterThanOrEqualTo(0)
                      .LessThanOrEqualTo(100);

        }
    }
}

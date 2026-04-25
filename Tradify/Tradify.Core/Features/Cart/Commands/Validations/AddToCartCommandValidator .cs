using System;
using System.Collections.Generic;
using System.Text;
    using FluentValidation;
    using Tradify.Core.Features.Cart.Commands.Models;
    using Tradify.Core.Resources.Service;
namespace Tradify.Core.Features.Cart.Commands.Validations
{


    public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
    {
        #region Fields
        private readonly LocalizationService localize;
        #endregion

        #region Constructor
        public AddToCartCommandValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyValidations();
        }
        #endregion

        #region Validations
        public void ApplyValidations()
        {
            RuleFor(x => x.CartId)
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .GreaterThan(0).WithMessage(localize.Get("InvalidId"));

            RuleFor(x => x.ProductVariant)
                .NotNull().WithMessage(localize.Get("Required"));

            RuleFor(x => x.ProductVariant)
                .SetValidator(new ProductVariantRequestToCartValidator(localize));
        }
        #endregion
    }
}
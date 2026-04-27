using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using Tradify.Core.Features.Cart.Commands.Models;
using Tradify.Core.Resources.Service;
namespace Tradify.Core.Features.Cart.Commands.Validations
{
    

    public class ProductVariantRequestToCartValidator : AbstractValidator<ProductVariantRequestToCart>
    {
        #region Fields
        private readonly LocalizationService localize;
        #endregion

        #region Constructor
        public ProductVariantRequestToCartValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyValidations();
        }
        #endregion

        #region Validations
        public void ApplyValidations()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .GreaterThan(0).WithMessage(localize.Get("InvalidId"));

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .GreaterThan(0).WithMessage(localize.Get("InvalidId"));

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(localize.Get("Required"))
                .MaximumLength(200).WithMessage(localize.Get("MaxLength"));

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage(localize.Get("InvalidPrice"));

            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0).WithMessage(localize.Get("InvalidDiscount"));

            RuleFor(x => x.Color)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.Color));

            RuleFor(x => x.Size)
                .MaximumLength(50).When(x => !string.IsNullOrEmpty(x.Size));
        }
        #endregion
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Discount.Product.Command.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Discount.Product.Command.Validations
{
    public class DeleteProductDiscountValidator : AbstractValidator<DeleteProductDiscountCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public DeleteProductDiscountValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductValidations();
        }


        #endregion

        #region Validations
        public void ApplyProductValidations()
        {
            RuleFor(x => x.ProductId)
                 .GreaterThan(0).WithMessage(localize.Get("IdMustBeGreaterThanZero"))
                 .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                 .NotNull().WithMessage(localize.Get("Required"));


        }
        #endregion
    }
}

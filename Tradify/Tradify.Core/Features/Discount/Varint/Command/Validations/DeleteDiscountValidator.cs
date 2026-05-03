using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Discount.Varint.Command.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Discount.Varint.Command.Validations
{
    public class DeleteDiscountValidator : AbstractValidator<DeleteDiscountCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public DeleteDiscountValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductValidations();
        }


        #endregion

        #region Validations
        public void ApplyProductValidations()
        {
            RuleFor(x => x.VariantId)
                 .GreaterThan(0).WithMessage(localize.Get("IdMustBeGreaterThanZero"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .NotNull().WithMessage(localize.Get("Required"));


        }
        #endregion
    }
}


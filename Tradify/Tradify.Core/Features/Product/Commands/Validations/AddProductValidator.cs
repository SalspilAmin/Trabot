using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Product.Commands.Validations
{
    public class AddProductValidator : AbstractValidator<AddProductCommand>
    {

        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddProductValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductValidations();
        }


        #endregion

        #region Validations
        public void ApplyProductValidations()
        {

            RuleFor(x => x.Name)
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .MaximumLength(255).WithMessage(localize.Get("MaxLengthis255")); 

            RuleFor(x => x.Description)
                 .NotNull().WithMessage(localize.Get("Required"))
                 .MaximumLength(1000).WithMessage(localize.Get("MaxLengthis1000"));


            RuleFor(x => x.CategoryId)
                 .GreaterThan(0).WithMessage(localize.Get("IdMustBeGreaterThanZero"))
                 .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                 .NotNull().WithMessage(localize.Get("Required"));


            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage(localize.Get("PriceGreaterThanZero"))
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"));
            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage(localize.Get("StockGreaterThanZero"))
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"));
        }
        #endregion
    }
}


using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Product.Commands.Validations
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        #region Fields
        private readonly LocalizationService localize;
        #endregion

        #region Constructor
        public UpdateProductValidator(LocalizationService localization)
        {
            localize = localization;
            ApplyValidations();
        }
        #endregion

        #region Validations
        private void ApplyValidations()
        {
            RuleFor(x => x.ProductId)
            .GreaterThan(0).WithMessage(localize.Get("Required"));

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .MaximumLength(255).WithMessage(localize.Get("MaxLengthis255"));

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(localize.Get("NotEmpty"));

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage(localize.Get("Required"));

            #endregion
        }
    }
}

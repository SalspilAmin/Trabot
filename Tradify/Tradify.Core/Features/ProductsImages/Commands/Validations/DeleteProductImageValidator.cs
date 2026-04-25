using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.ProductsImages.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.ProductsImages.Commands.Validations
{
    public class DeleteProductImageValidator : AbstractValidator<DeleteProductImageCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public DeleteProductImageValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductValidations();
        }


        #endregion

        #region Validations
        public void ApplyProductValidations()
        {

            RuleFor(x => x.Id)
           .NotEmpty().WithMessage(localize.Get("Required"))
           .GreaterThan(0).WithMessage(localize.Get("MustBeGreaterThanZero")); ;

        }
        #endregion
    
    }
}

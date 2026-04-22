using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.ProductsImages.Commands.Models;
using Tradify.Core.Features.StoreImage.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.StoreImage.Commands.Validations
{
    public class DeleteStoreImageValidator : AbstractValidator<DeleteStoreImageCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public DeleteStoreImageValidator(LocalizationService localization)
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
           .GreaterThan(0).WithMessage(localize.Get("MustBeGreaterThanZero"));

          //  RuleFor(x => x.SellerId)
          //.NotEmpty().WithMessage(localize.Get("Required"))
          //.GreaterThan(0).WithMessage(localize.Get("MustBeGreaterThanZero"));


        }
        #endregion
    
    }
}

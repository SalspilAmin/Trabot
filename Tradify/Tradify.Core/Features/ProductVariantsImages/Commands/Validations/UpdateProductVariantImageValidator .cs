using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.ProductsImages.Commands.Models;
using Tradify.Core.Features.ProductVariantsImages.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.ProductVariantsImages.Commands.Validations
{
    public class UpdateProductVariantImageValidator : AbstractValidator<UpdateProductVariantImageCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public UpdateProductVariantImageValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductValidations();
        }


        #endregion

        #region Validations
        public void ApplyProductValidations()
        {
           


            RuleFor(x => x.ImageId)
                    .NotEmpty().WithMessage(localize.Get("Required"));
            


       


        }
        #endregion
    }
}

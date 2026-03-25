using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.ProductsImages.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.ProductsImages.Commands.Validations
{
    public class AddProductImageValidator : AbstractValidator<AddProductImageCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddProductImageValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductValidations();
        }


        #endregion

        #region Validations
        public void ApplyProductValidations()
        {

            RuleFor(x => x.ProductId)
            .NotEmpty().WithMessage(localize.Get("Required"));


            RuleFor(x => x.Image)
    .NotNull().WithMessage(localize.Get("Required"))
    .DependentRules(() =>
    {
        RuleFor(x => x.Image.Length)
            .LessThanOrEqualTo(5 * 1024 * 1024)
            .WithMessage(localize.Get("MaxSizeIs5MB"));

        RuleFor(x => x.Image)
            .Must(x => x.ContentType == "image/jpeg"
                    || x.ContentType == "image/png"
                    || x.ContentType == "image/webp")
            .WithMessage(localize.Get("OnlyImagesAllowed"));
    });


        }
        #endregion
    }
}

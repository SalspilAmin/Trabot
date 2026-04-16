using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.ProductsImages.Commands.Models;
using Tradify.Core.Features.ProductVariantsImages.Commands.Models;
using Tradify.Core.Features.StoreImage.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.ProductVariantsImages.Commands.Validations
{
    public class AddProductVariantImageValidator : AbstractValidator<AddProductVariantImageCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddProductVariantImageValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductValidations();
        }


        #endregion

        #region Validations
        public void ApplyProductValidations()
        {

            RuleFor(x => x.ProductVariantId)
            .NotEmpty().WithMessage(localize.Get("Required"));

            //RuleFor(x => x.SellerId)
            //.NotEmpty().WithMessage(localize.Get("Required"));


            RuleFor(x => x.Image)
   .NotNull().WithMessage(localize.Get("Required"))
   .Must(file => file != null && file.Length > 0)
   .WithMessage(localize.Get("NoFile"))
   .Must(file => file.ContentType.StartsWith("image/"))
   .WithMessage(localize.Get("OnlyImagesAllowed"))
   .Must(file =>
   {
       var ext = Path.GetExtension(file.FileName).ToLower();
       return ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".webp";
   })
   .WithMessage(localize.Get("OnlyImagesAllowed"))
   .Must(file => file.Length <= 5 * 1024 * 1024)
   .WithMessage(localize.Get("MaxSizeIs5MB"));





        }
        #endregion
    }
}


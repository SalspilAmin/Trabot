using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.ProductsImages.Commands.Models;
using Tradify.Core.Features.StoreImage.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.StoreImage.Commands.Validations
{
    public class AddStoreImageValidator : AbstractValidator<AddStoreImageCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddStoreImageValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyValidations();
        }


        #endregion

        #region Validations
        public void ApplyValidations()
        {

            RuleFor(x => x.StoreId)
            .NotEmpty().WithMessage(localize.Get("Required"));




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














    //        RuleFor(x => x.Image)
    //.NotNull().WithMessage(localize.Get("Required"))
    //.DependentRules(() =>
    //{
    //    RuleFor(x => x.Image.Length)
    //        .LessThanOrEqualTo(5 * 1024 * 1024)
    //        .WithMessage(localize.Get("MaxSizeIs5MB"));

    //    RuleFor(x => x.Image)
    //        .Must(x => x.ContentType == "image/jpeg"
    //                || x.ContentType == "image/png"
    //                || x.ContentType == "image/webp")
    //        .WithMessage(localize.Get("OnlyImagesAllowed"));

    //    RuleFor(x => x.Image.FileName)
    //               .Must(fileName =>
    //                fileName.EndsWith(".jpg") ||
    //    fileName.EndsWith(".jpeg") ||
    //    fileName.EndsWith(".png") ||
    //    fileName.EndsWith(".webp"))
    //.WithMessage(localize.Get("OnlyImagesAllowed"));
    //});


        }
        #endregion
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.ProductVariant.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.ProductVariant.Commands.Validations
{
    public class AddProductVarintWithImageValidator : AbstractValidator<AddProductVarintWithImageCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddProductVarintWithImageValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductVariantValidator();
        }


        #endregion
        public void ApplyProductVariantValidator()
        {
            //RuleFor(x => x.StoreId)
            //   .NotEmpty().WithMessage(localize.Get("NotEmpty"))
            //   .GreaterThan(0).WithMessage(localize.Get("ProductIdGreaterThanZero"));



            RuleFor(x => x.Price).GreaterThan(0).WithMessage(localize.Get("PriceGreaterThanZero"));
            RuleFor(x => x.NumberOfProductInStock).GreaterThanOrEqualTo(0).WithMessage(localize.Get("StockGreaterThanZero"));

            RuleFor(x => x.ProductId)
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .GreaterThan(0).WithMessage(localize.Get("ProductIdGreaterThanZero"));
            RuleFor(x => x.Discount)
                      .GreaterThanOrEqualTo(0)
                      .LessThanOrEqualTo(100);

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
    }
}

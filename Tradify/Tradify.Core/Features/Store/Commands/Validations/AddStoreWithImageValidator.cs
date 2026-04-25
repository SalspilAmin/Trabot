using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Store.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Store.Commands.Validations
{
    public class AddStoreWithImageValidator : AbstractValidator<AddStoreWithImageCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddStoreWithImageValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyValidation();
        }


        #endregion
        #region Validations
        public void ApplyValidation()
        {
            RuleFor(x => x.Name)
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .MaximumLength(255).WithMessage(localize.Get("MaxLengthis255"));

            RuleFor(x => x.Description)
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .MaximumLength(1000).WithMessage(localize.Get("MaxLengthis1000"));

            RuleFor(x => x.SellerId)

                .GreaterThan(0).WithMessage(localize.Get("IdMustBeGreaterThanZero"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .NotNull().WithMessage(localize.Get("Required"));

            RuleFor(x => x.Image)
                .NotNull().WithMessage(localize.Get("Required"))
                .DependentRules(() =>
                {
                    RuleFor(x => x.Image.Length)
                        .GreaterThan(0).WithMessage(localize.Get("NoFile"))
                        .LessThanOrEqualTo(5 * 1024 * 1024)
                        .WithMessage(localize.Get("MaxSizeIs5MB"));

                    RuleFor(x => x.Image)
                        .Must(file => file.ContentType.StartsWith("image/"))
                        .WithMessage(localize.Get("OnlyImagesAllowed"));

                    RuleFor(x => x.Image)
                        .Must(file =>
                        {
                            var ext = Path.GetExtension(file.FileName).ToLower();
                            return ext == ".jpg" || ext == ".jpeg" || ext == ".png" || ext == ".webp" || ext == ".jfif";
                        })
                        .WithMessage(localize.Get("OnlyImagesAllowed"));
                });



        }

        #endregion
    }
}

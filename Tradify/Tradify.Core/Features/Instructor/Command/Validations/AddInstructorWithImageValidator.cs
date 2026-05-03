using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Instructor.Command.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Instructor.Command.Validations
{
    public class AddInstructorWithImageValidator : AbstractValidator<AddInstructorWithImageCommand>
    {

        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddInstructorWithImageValidator(LocalizationService localization)
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

            RuleFor(x => x.JobTitle)
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .MaximumLength(255).WithMessage(localize.Get("MaxLengthis255"));

            RuleFor(x => x.About)
                 .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))

                 .MaximumLength(1000).WithMessage(localize.Get("MaxLengthis1000"));


            RuleFor(x => x.UserId)
                 .GreaterThan(0).WithMessage(localize.Get("IdMustBeGreaterThanZero"))
                 .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                 .NotNull().WithMessage(localize.Get("Required"));


            RuleFor(x => x.PricePerSession)
                .GreaterThan(0).WithMessage(localize.Get("PricePerSessionGreaterThanZero"))
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"));

            RuleFor(x => x.YearsOfExperience)
                .GreaterThanOrEqualTo(0).WithMessage(localize.Get("YearsOfExperienceGreaterThanZero"))
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"));

            RuleFor(x => x.Discount)
                     .GreaterThanOrEqualTo(0)
                     .LessThanOrEqualTo(100);

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

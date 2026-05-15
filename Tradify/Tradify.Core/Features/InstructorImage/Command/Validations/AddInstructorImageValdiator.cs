using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.InstructorImage.Command.Models;
using Tradify.Core.Features.StoreImage.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.InstructorImage.Command.Validations
{
    public class AddInstructorImageValdiator : AbstractValidator<AddInstructorImageCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddInstructorImageValdiator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyValidations();
        }


        #endregion

        #region Validations
        public void ApplyValidations()
        {

            RuleFor(x => x.InstructorId)
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

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Post.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Post.Commands.Validations
{
    public class AddPostValidator : AbstractValidator<AddPostModelCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddPostValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyPostValidations();
        }


        #endregion

        #region Validations
        public void ApplyPostValidations()
        {
            // 🔹 UserId
            RuleFor(x => x.UserId)
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"));


            RuleFor(x => x)
                .Must(x => !string.IsNullOrWhiteSpace(x.Content)
                        || !string.IsNullOrWhiteSpace(x.Caption))
                 .WithMessage(localize.Get("Required"));
              

            // 🔹 Content length
            RuleFor(x => x.Content)
                .MaximumLength(1000)
                .WithMessage(localize.Get("MaxLengthis1000"));

            // 🔹 Caption length
            RuleFor(x => x.Caption)
                .MaximumLength(1000)
                .WithMessage(localize.Get("MaxLengthis1000"));

            // 🔹 Media Files (optional but validated if exists)
            RuleFor(x => x.MediaFilles)
                .Must(files => files == null || files.Count <= 4)
                .WithMessage(localize.Get("MaxNumberOfFilles4"));
                
            // 🔹 Each file validation
            RuleForEach(x => x.MediaFilles).ChildRules(file =>
            {
                // File size (e.g. max 20MB)
                file.RuleFor(f => f.Length)
                    .LessThanOrEqualTo(20 * 1024 * 1024)
                    .WithMessage(localize.Get("MaxSizeIs100MB"));

                // Allowed extensions
                file.RuleFor(f => f.FileName)
                    .Must(BeValidExtension)
                    .WithMessage(localize.Get("Allowedextensions"));
            });
        }

        #endregion

        #region Helpers

        private bool BeValidExtension(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
            var extension = System.IO.Path.GetExtension(fileName).ToLower();

            return allowedExtensions.Contains(extension);
        }

        #endregion
    }
}

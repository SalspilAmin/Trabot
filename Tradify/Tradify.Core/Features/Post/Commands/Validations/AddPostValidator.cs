using FluentValidation;
using Tradify.Core.Features.Post.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Post.Commands.Validations
{
    public class AddPostValidator : AbstractValidator<AddPostModelCommand>
    {
        private readonly LocalizationService localize;

        public AddPostValidator(LocalizationService localization)
        {
            localize = localization;
            ApplyPostValidations();
        }

        public void ApplyPostValidations()
        {
            // UserId
            RuleFor(x => x.UserId)
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"));

            // At least Content OR Caption OR Files
            RuleFor(x => x)
                .Must(x =>
                    !string.IsNullOrWhiteSpace(x.Content) ||
                    !string.IsNullOrWhiteSpace(x.Caption) ||
                    (x.MediaFilles != null && x.MediaFilles.Any()))
                .WithMessage(localize.Get("Required"));

            // Content length
            RuleFor(x => x.Content)
                .MaximumLength(1000)
                .When(x => !string.IsNullOrWhiteSpace(x.Content))
                .WithMessage(localize.Get("MaxLengthis1000"));

            // Caption length
            RuleFor(x => x.Caption)
                .MaximumLength(1000)
                .When(x => !string.IsNullOrWhiteSpace(x.Caption))
                .WithMessage(localize.Get("MaxLengthis1000"));

            // Maximum 4 files
            RuleFor(x => x.MediaFilles)
                .Must(files => files == null || files.Count <= 4)
                .WithMessage(localize.Get("MaxNumberOfFilles4"));

            // Validate files only if they exist
            When(x => x.MediaFilles != null && x.MediaFilles.Any(), () =>
            {
                RuleForEach(x => x.MediaFilles).ChildRules(file =>
                {
                    file.RuleFor(f => f.Length)
                        .LessThanOrEqualTo(20 * 1024 * 1024)
                        .WithMessage(localize.Get("MaxSizeIs100MB"));

                    file.RuleFor(f => f.FileName)
                        .Must(BeValidExtension)
                        .WithMessage(localize.Get("Allowedextensions"));
                });
            });
        }

        private bool BeValidExtension(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return false;

            var allowedExtensions = new[]
            {
                ".jpg",
                ".jpeg",
                ".png",
                ".gif",
                ".webp",
                ".mp4",
                ".mov",
                ".avi",
                ".wmv"
            };

            var extension = Path.GetExtension(fileName).ToLower();

            return allowedExtensions.Contains(extension);
        }
    }
}
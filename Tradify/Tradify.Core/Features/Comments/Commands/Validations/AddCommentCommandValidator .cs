using FluentValidation;
using Tradify.Core.Features.Comments.Commands.Models;

namespace Tradify.Core.Features.Comments.Commands.Validations
{
    public class AddCommentCommandValidator : AbstractValidator<AddCommentCommand>
    {
        public AddCommentCommandValidator()
        {
            RuleFor(x => x.PostId)
                .GreaterThan(0)
                .WithMessage("PostId must be greater than 0.");

            RuleFor(x => x.UserId)
                .GreaterThan(0)
                .WithMessage("UserId must be greater than 0.");

            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Content is required.")
                .MaximumLength(1000)
                .WithMessage("Content cannot exceed 1000 characters.");
        }
    }
}
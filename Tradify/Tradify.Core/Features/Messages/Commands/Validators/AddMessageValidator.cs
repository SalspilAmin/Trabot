using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Messages.Commands.Models;

namespace Tradify.Core.Features.Messages.Commands.Validators
{
    public class AddMessageValidator :
    AbstractValidator<AddMessageCommand>
    {
        public AddMessageValidator()
        {
            RuleFor(x => x.SenderId)
                .GreaterThan(0);

            RuleFor(x => x.ReceiverId)
                .GreaterThan(0);

            RuleFor(x => x)
                .Must(x =>
                    !string.IsNullOrWhiteSpace(x.Content)
                    ||
                    (x.MediaFiles != null &&
                     x.MediaFiles.Any()))
                .WithMessage("Message must contain content or media");
        }
    }
}

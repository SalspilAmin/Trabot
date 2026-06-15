using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Messages.Commands.Models;

namespace Tradify.Core.Features.Messages.Commands.Validators
{
    public class UpdateMessageValidator :
    AbstractValidator<UpdateMessageCommand>
    {
        public UpdateMessageValidator()
        {
            RuleFor(x => x.MessageId)
                .GreaterThan(0);

            RuleFor(x => x.Content)
                .NotEmpty();
        }
    }
}

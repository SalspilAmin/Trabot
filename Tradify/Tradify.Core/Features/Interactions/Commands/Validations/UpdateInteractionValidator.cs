using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Interactions.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Interactions.Commands.Validations
{
    public class UpdateInteractionValidator :
     AbstractValidator<
         UpdateInteractionWithPostCommand>
    {

        private readonly LocalizationService localize;
        public UpdateInteractionValidator(LocalizationService localization)
        {
            this.localize = localization;
            UpdateInteraction();
        }
        public void UpdateInteraction()
        {

            RuleFor(x => x.InteractionId)
             .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"));

            RuleFor(x => x.InteractionBy)
                 .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"));
        }
    }
}

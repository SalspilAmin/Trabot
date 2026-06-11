using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Interactions.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Interactions.Commands.Validations
{
    public class AddInteractionValidator :
         AbstractValidator<AddInteractionWithPostCommand>
    {

        #region Fields 

        private readonly LocalizationService localize;
        #endregion

      
        public AddInteractionValidator(LocalizationService localization)
        {
            this.localize = localization;
            AddInteraction();
        }
        public void AddInteraction()
        {
            RuleFor(x => x.UserId)
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"));

            RuleFor(x => x.PostId)
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"));

            RuleFor(x => x.InteractionBy)
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"));
        }

    }
}



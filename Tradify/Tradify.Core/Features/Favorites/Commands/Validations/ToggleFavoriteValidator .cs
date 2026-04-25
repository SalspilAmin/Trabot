using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Favorites.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Favorites.Commands.Validations
{
    public class ToggleFavoriteValidator : AbstractValidator<ToggleFavoriteCommand>
    {

        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public ToggleFavoriteValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductValidations();
        }


        #endregion

        #region Validations
        public void ApplyProductValidations()
        {

            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage(localize.Get("Required"));



        }

        #endregion
    }
}

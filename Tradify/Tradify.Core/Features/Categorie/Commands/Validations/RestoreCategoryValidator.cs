using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Categorie.Commands.Models;
using Tradify.Core.Features.ProductVariant.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Categorie.Commands.Validations
{
    public class RestoreCategoryValidator : AbstractValidator< RestoreCategoryCommand>
    {

        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public RestoreCategoryValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductValidations();
        }


        #endregion

        #region Validations
        public void ApplyProductValidations()
        {


            RuleFor(x => x.Id)
             .GreaterThan(0)
             .WithMessage(localize.Get("Required"));
        }

        #endregion

    }
}

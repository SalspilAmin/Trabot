using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Categorie.Commands.Models;
using Tradify.Core.Features.ProductVariant.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Categorie.Commands.Validations
{
    public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommand>
    {
        #region Fields
        private readonly LocalizationService localize;
        #endregion

        #region Constructor
        public DeleteCategoryValidator(LocalizationService localization)
        {
            localize = localization;
            ApplyValidations();
        }
        #endregion

        #region Validations
        private void ApplyValidations()
        {


            RuleFor(x => x.Id)
               .GreaterThan(0).WithMessage(localize.Get("Required"));

        }

        #endregion

    }
}

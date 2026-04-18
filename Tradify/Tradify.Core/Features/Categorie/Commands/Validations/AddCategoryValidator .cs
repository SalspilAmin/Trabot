using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Categorie.Commands.Models;
using Tradify.Core.Features.Favorites.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Categorie.Commands.Validations
{
    public class AddCategoryValidator : AbstractValidator<AddCategoryCommand>
    {

        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddCategoryValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductValidations();
        }


        #endregion

        #region Validations
        public void ApplyProductValidations()
        {

            //RuleFor(x => x.SellerId)
            //    .GreaterThan(0).WithMessage(localize.Get("IdMustBeGreaterThanZero"))
            //    .NotEmpty().WithMessage(localize.Get("NotEmpty"))
            //    .NotNull().WithMessage(localize.Get("Required"));


            RuleFor(x => x.Name)
                .NotNull().WithMessage(localize.Get("Required"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .MaximumLength(255).WithMessage(localize.Get("MaxLengthis255"));


            RuleFor(x => x.ParentCategoryId).GreaterThan(0).When(x => x.ParentCategoryId.HasValue)
            .WithMessage(localize.Get("ParentCategoryIdMustGreaterThan0"));


        }
        #endregion
    }
}

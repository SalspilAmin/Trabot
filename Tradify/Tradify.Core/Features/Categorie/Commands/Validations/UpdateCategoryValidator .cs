using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Categorie.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Categorie.Commands.Validations
{
    public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommand>
    {

        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public UpdateCategoryValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductValidations();
        }


        #endregion

        #region Validations
        public void ApplyProductValidations()
        {



            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage(localize.Get("Required"));

            RuleFor(x => x.Name)
               .NotEmpty().WithMessage(localize.Get("Required"))
               .MaximumLength(100).WithMessage(localize.Get("MaxLengthis100"));


            RuleFor(x => x.ParentCategoryId).GreaterThan(0).When(x => x.ParentCategoryId.HasValue)
           .WithMessage(localize.Get("ParentCategoryIdMustGreaterThan0"));

            // ❗ أهم حاجة: تمنعي self-parent
            RuleFor(x => x)
                .Must(x => x.ParentCategoryId != x.Id)
                .WithMessage("CategoryCannotBeParentOfItself");


        }
        #endregion
    }
}

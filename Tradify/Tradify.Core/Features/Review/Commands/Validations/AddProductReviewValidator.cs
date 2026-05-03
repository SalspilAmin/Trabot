using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Review.Commands.Models;
using Tradify.Core.Features.Store.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Review.Commands.Validations
{
    public class AddProductReviewValidator : AbstractValidator<AddProductReviewCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddProductReviewValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyValidation();
        }


        #endregion
        #region Validations
        public void ApplyValidation()
        {


            RuleFor(x => x.ProductId)
                .GreaterThan(0).WithMessage(localize.Get("IdMustBeGreaterThanZero"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .NotNull().WithMessage(localize.Get("Required"));

            RuleFor(x => x.Rating)
               .NotEmpty().WithMessage(localize.Get("NotEmpty"))
              .NotNull().WithMessage(localize.Get("Required"));



            RuleFor(x => x.Comment)
                .MaximumLength(1000).WithMessage(localize.Get("MaxLengthis1000"));
        }

        #endregion
    }
}
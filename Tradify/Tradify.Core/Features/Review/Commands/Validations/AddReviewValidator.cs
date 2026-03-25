using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Review.Commands.Models;
using Tradify.Core.Features.Store.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Review.Commands.Validations
{
    public class AddReviewValidator : AbstractValidator<AddReviewCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddReviewValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyValidation();
        }


        #endregion
        #region Validations
        public void ApplyValidation()
        {
        

            RuleFor(x => x.ProductId)
               .GreaterThan(0).WithMessage(localize.Get("Required"));

            RuleFor(x => x.Rating)
               .Must(r => r >= 1 && r <= 5).WithMessage(localize.Get("InvalidRatingValue"));

           

            RuleFor(x => x.Comment)
                .MaximumLength(1000).WithMessage(localize.Get("CommentTooLong"));
        }

        #endregion
    }
}

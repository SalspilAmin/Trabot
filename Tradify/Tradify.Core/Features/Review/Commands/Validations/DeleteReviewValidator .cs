using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Review.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Review.Commands.Validations
{
    public class DeleteReviewValidator : AbstractValidator<DeleteReviewCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public DeleteReviewValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyValidation();
        }


        #endregion
        #region Validations
        public void ApplyValidation()
        {


            RuleFor(x => x.Id)
                .GreaterThan(0)
                .WithMessage(localize.Get("Required"));
        }

        #endregion
    }
}

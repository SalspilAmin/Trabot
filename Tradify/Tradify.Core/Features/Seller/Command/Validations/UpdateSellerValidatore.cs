using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Instructor.Command.Models;
using Tradify.Core.Features.Seller.Command.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Seller.Command.Validations
{
    public class UpdateSellerValidatore : AbstractValidator<UpdateSellerCommand>
    {

        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public UpdateSellerValidatore(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductValidations();
        }


        #endregion

        #region Validations
        public void ApplyProductValidations()
        {

            RuleFor(x => x.BusinessName)
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
              .NotNull().WithMessage(localize.Get("Required"))
              .MaximumLength(100).WithMessage(localize.Get("MaxLengthis100"));

            RuleFor(x => x.BusinessType)
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
             .NotNull().WithMessage(localize.Get("Required"))
             .MaximumLength(100).WithMessage(localize.Get("MaxLengthis100"));

        }
        #endregion
    }
}

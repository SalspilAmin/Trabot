using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Seller.Command.Models;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Seller.Command.Validations
{
    public class AddSellerValidation : AbstractValidator<AddSellerCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddSellerValidation(LocalizationService localization)
        {
            this.localize = localization;
            ApplyValidations();
        }


        #endregion

        #region Validations


        public void ApplyValidations()
        {

            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage(localize.Get("IdMustBeGreaterThanZero"))
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .NotNull().WithMessage(localize.Get("Required"));


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

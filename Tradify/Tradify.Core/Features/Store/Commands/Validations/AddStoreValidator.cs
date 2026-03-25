using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.Store.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Store.Commands.Validations
{
    public class AddStoreValidator : AbstractValidator<AddStoreCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddStoreValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyValidation();
        }


        #endregion
        #region Validations
        public void ApplyValidation()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .MaximumLength(255).WithMessage(localize.Get("MaxLengthis255"));

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .MaximumLength(1000).WithMessage(localize.Get("MaxLengthis1000")); 

            RuleFor(x => x.SellerId)
                .GreaterThan(0).WithMessage(localize.Get("Required"));
            RuleFor(x => x.IsActive)
                .NotNull().WithMessage(localize.Get("Required"))
                ;



        }

        #endregion
    }
}




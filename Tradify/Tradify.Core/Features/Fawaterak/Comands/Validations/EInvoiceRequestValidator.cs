using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Fawaterak.Comands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Fawaterak.Comands.Validations
{
    public class EInvoiceRequestValidator : AbstractValidator<EInvoiceRequestLinkCommand>
    {
        private readonly LocalizationService localization;
        public EInvoiceRequestValidator(LocalizationService localization)
        {
            this.localization = localization;
            ApplyValidationsRules();
        }

        public void ApplyValidationsRules()
        {
            
            //RuleFor(x => x.Customer.FirstName).NotEmpty().WithMessage(localization.Get("NotEmpty"))
            //    .NotNull().WithMessage(localization.Get("Required"));
            //RuleFor(x => x.Customer.LastName).NotEmpty().WithMessage(localization.Get("NotEmpty"))
            //    .NotNull().WithMessage(localization.Get("Required"));
            //RuleFor(x => x.Customer).NotEmpty().WithMessage(localization.Get("NotEmpty"))
            //    .NotNull().WithMessage(localization.Get("Required"));
            
            //RuleFor(x => x.CartTotal).NotEmpty().WithMessage(localization.Get("NotEmpty"))
            //    .NotNull().WithMessage(localization.Get("Required"));
            //RuleFor(x => x.CartItems).NotEmpty().WithMessage(localization.Get("NotEmpty"))
            //    .NotNull().WithMessage(localization.Get("Required"));

        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Order.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Order.Commands.Validations
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderCommandModel>
    {
        private readonly LocalizationService localization;

        public UpdateOrderValidator(LocalizationService localization)

        {
            this.localization = localization;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {

            RuleFor(x => x.Id).NotEmpty().WithMessage(localization.Get("NotEmpty"))
                   .NotNull().WithMessage(localization.Get("Required"));
            RuleFor(x => x.invoice_id).NotEmpty().WithMessage(localization.Get("NotEmpty"))
                .NotNull().WithMessage(localization.Get("Required"));
            RuleFor(x => x.invoice_key).NotEmpty().WithMessage(localization.Get("NotEmpty"))
                .NotNull().WithMessage(localization.Get("Required"));
            RuleFor(x => x.PaymentStatus).NotEmpty().WithMessage(localization.Get("NotEmpty"))
                .NotNull().WithMessage(localization.Get("Required"));
           

        }
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Order.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Order.Commands.Validations
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderModel>
    {
        private readonly LocalizationService localization;

        public CreateOrderValidator(LocalizationService localization)

        {
            this.localization = localization;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {

          RuleFor(x=>x.CustomerId).NotEmpty().WithMessage(localization.Get("NotEmpty"))
                 .NotNull().WithMessage(localization.Get("Required"));
            RuleFor(x => x.ShippingAddress).NotEmpty().WithMessage(localization.Get("NotEmpty"))
                .NotNull().WithMessage(localization.Get("Required"));
            RuleFor(x => x.CartId).NotEmpty().WithMessage(localization.Get("NotEmpty"))
                .NotNull().WithMessage(localization.Get("Required"));
            RuleFor(x => x.ShippingPrice).NotEmpty().WithMessage(localization.Get("NotEmpty"))
                .NotNull().WithMessage(localization.Get("Required"));
            RuleFor(x => x.TotalAmount).NotEmpty().WithMessage(localization.Get("NotEmpty"))
                .NotNull().WithMessage(localization.Get("Required"))
                .GreaterThan(x => x.ShippingPrice).WithMessage(localization.Get("AddCorrectValue"));
   
          }
    }
}

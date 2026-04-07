using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Order.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Order.Commands.Validations
{
    public class DeleteOrderValidator : AbstractValidator<DeleteOrderCommand>
    {
        private readonly LocalizationService localization;

        public DeleteOrderValidator(LocalizationService localization)

        {
            this.localization = localization;
            ApplyValidationsRules();
        }
        public void ApplyValidationsRules()
        {

            RuleFor(x => x.OrderId).GreaterThan(0).WithMessage(localization.Get("Required"));
           

        }
    }
}

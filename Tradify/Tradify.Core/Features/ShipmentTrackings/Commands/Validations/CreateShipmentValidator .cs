using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.ShipmentTrackings.Commands.Models;
using Tradify.Core.Features.Store.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.ShipmentTrackings.Commands.Validations
{
    public class CreateShipmentValidator : AbstractValidator<CreateShipmentCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public CreateShipmentValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyValidation();
        }


        #endregion
        #region Validations
        public void ApplyValidation()
        {
            RuleFor(x => x.OrderId)
                .GreaterThan(0)
                .WithMessage(localize.Get("Required"));


        }

        #endregion
    }
}

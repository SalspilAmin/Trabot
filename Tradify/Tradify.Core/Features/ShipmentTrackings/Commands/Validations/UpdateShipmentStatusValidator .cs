using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.ShipmentTrackings.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.ShipmentTrackings.Commands.Validations
{
    internal class UpdateShipmentStatusValidator : AbstractValidator<UpdateShipmentStatusCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public UpdateShipmentStatusValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyValidation();
        }


        #endregion
        #region Validations
        public void ApplyValidation()
        {
            RuleFor(x => x.ShipmentTrackingId)
             .GreaterThan(0).WithMessage(localize.Get("Required"));

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage(localize.Get("InvalidShipmentStatus"));


        }

        #endregion
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Booking.Command.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Booking.Command.Validations
{
    public class CancelBookingValidator : AbstractValidator<CancelBookingCommand>
    {

        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public CancelBookingValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductValidations();
        }


        #endregion

        #region Validations
        public void ApplyProductValidations()
        {


            RuleFor(x => x.BookingId)
                 .GreaterThan(0).WithMessage(localize.Get("IdMustBeGreaterThanZero"))
                 .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                 .NotNull().WithMessage(localize.Get("Required"));


        }
        #endregion
    }
}

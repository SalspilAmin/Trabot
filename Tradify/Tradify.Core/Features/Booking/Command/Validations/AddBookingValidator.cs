using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Booking.Command.Models;
using Tradify.Core.Features.Categorie.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Booking.Command.Validations
{
    public class AddBookingValidator   : AbstractValidator<AddBookingCommand>
    {

        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddBookingValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyProductValidations();
        }


        #endregion

        #region Validations
        public void ApplyProductValidations()
        {


            RuleFor(x => x.ScheduleId)
                 .GreaterThan(0).WithMessage(localize.Get("IdMustBeGreaterThanZero"))
                 .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                 .NotNull().WithMessage(localize.Get("Required"));


        }
        #endregion
    }
}

using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Store.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Store.Commands.Validations
{
    public class UpdateStoreValidator : AbstractValidator<UpdateStoreCommand>
    {  
        
        #region Fields
        private readonly LocalizationService localize;
        #endregion

        #region Constructor
        public UpdateStoreValidator(LocalizationService localization)
        {
            localize = localization;
            ApplyValidations();
        }
        #endregion

        #region Validations
        private void ApplyValidations()
        {
            

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
               .NotNull().WithMessage(localize.Get("Required"))

                .MaximumLength(1000).WithMessage(localize.Get("MaxLengthis1000"));
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage(localize.Get("NotEmpty"))
               .NotNull().WithMessage(localize.Get("Required"))
               .MaximumLength(255).WithMessage(localize.Get("MaxLengthis255"));


            #endregion
        }
    }
}

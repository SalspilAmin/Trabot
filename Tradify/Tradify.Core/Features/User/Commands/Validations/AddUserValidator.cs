using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Core.Resources;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.User.Commands.Validations
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public AddUserValidator(LocalizationService localization)
        {
         this.localize = localization;          
        }


        #endregion

        #region Validations


        public void ApplyValidations()
        {
               RuleFor(x => x.UserName)
                .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                .NotNull().WithMessage(localize.Get("Required"))
                .MaximumLength(100).WithMessage(localize.Get("MaxLengthis100"));


            RuleFor(x => x.EmailOrPhone)
               .NotEmpty().WithMessage(localize.Get("NotEmpty"))
               .NotNull().WithMessage(localize.Get("Required"));
            RuleFor(x => x.Password)
                 .NotEmpty().WithMessage(localize.Get("NotEmpty"))
                 .NotNull().WithMessage(localize.Get("Required"));


        }
       

        #endregion


    }
}

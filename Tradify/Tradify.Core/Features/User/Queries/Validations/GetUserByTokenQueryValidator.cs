using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Seller.Command.Models;
using Tradify.Core.Features.User.Commands.Models;
using Tradify.Core.Features.User.Queries.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Seller.Command.Validations
{
    public class GetUserByTokenQueryValidator : AbstractValidator<GetUserByTokenQuery>
    {
        #region Fields 

        private readonly LocalizationService localize;
        #endregion

        #region constructor
        public GetUserByTokenQueryValidator(LocalizationService localization)
        {
            this.localize = localization;
            ApplyValidations();
        }


        #endregion

        #region Validations


        public void ApplyValidations()
        {




            RuleFor(x => x.AccessToken)
               .NotEmpty().WithMessage(localize.Get("NotEmpty"))
             .NotNull().WithMessage(localize.Get("Required"));
             

           


        }


        #endregion


    }
}

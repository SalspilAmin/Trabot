using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Authenticaiton.Commands.Models;
using Tradify.Core.Resources.Service;

namespace Tradify.Core.Features.Authenticaiton.Commands.Handler
{
    public class AuthenticationCommandHandler :ResponseHandler
     ,IRequestHandler<SignInCommand,Response<string>>
    {
        #region Fields
        private readonly LocalizationService localization;

        #endregion
        #region Constructor
        public AuthenticationCommandHandler(LocalizationService localization):base(localization) 
        {
            this.localization = localization;
        }

        

        #endregion


        #region Methods
      

      public  Task<Response<string>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
        #endregion

    }
}

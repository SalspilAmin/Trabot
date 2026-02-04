using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Authenticaiton.Queries.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities.Identity;
using Tradify.Service.AbstractsServices.AuthenticationServices;

namespace Tradify.Core.Features.Authenticaiton.Queries.Handler
{
    public class AuthenticationQueryHandler : ResponseHandler,IRequestHandler<ConfirmEmailQuery,Response<string>>,
        IRequestHandler<ConfirmPhoneQuery,Response<string>>
    {

        #region Fields
        private readonly LocalizationService _localization;       
        private readonly IAuthenticationService _authenticationService;
        private readonly UserManager<Tradify.Data.Entities.Identity.User> userManager;

        #endregion
        #region constructor

          public AuthenticationQueryHandler(LocalizationService localization,IAuthenticationService authenticationService
              , UserManager<Tradify.Data.Entities.Identity.User> userManager) : base(localization)
        {
            _localization = localization;       
            _authenticationService = authenticationService;
            this.userManager = userManager; 
        }

        
        
        #endregion
        #region Methods
           public async Task<Response<string>> Handle(ConfirmEmailQuery request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ConfirmEmailAsync(request.Id, request.Code);
            switch (result)
            {
                case "ErrorWhenConfirmEmail": return BadRequest<string>(_localization.Get("ErrorWhenConfirmEmail"));
                case "NotFound": return BadRequest<string>(_localization.Get("UserIsNotFound"));
                    
            }
           return Success<string>(_localization.Get("ConfirmEmailDone"));


        }

        public async Task<Response<string>> Handle(ConfirmPhoneQuery request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByIdAsync(request.Id.ToString());
            if(user == null) return BadRequest<string>(_localization.Get("UserIsNotFound"));
            if (user.OTP == request.OTP)
            {
                user.PhoneNumberConfirmed = true;
                userManager.UpdateAsync(user);
                return Success<string>(_localization.Get("ConfirmPhoneDone"));
            }
            else
            {
                return BadRequest<string>(_localization.Get("OTP_IsWrong"));
            }
        }



        #endregion


    }
}

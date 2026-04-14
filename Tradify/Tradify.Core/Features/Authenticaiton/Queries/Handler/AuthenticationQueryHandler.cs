using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Authenticaiton.Queries.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Helpers;
using Tradify.Service.AbstractsServices.AuthenticationServices;

namespace Tradify.Core.Features.Authenticaiton.Queries.Handler
{
    public class AuthenticationQueryHandler : ResponseHandler,IRequestHandler<ConfirmEmailQuery,Response<string>>,
         IRequestHandler<ConfirmPhoneQuery, Response<string>>,IRequestHandler<GetSignByGoogleResult,Response<LoginGoogleResult>>
    {

        #region Fields
        private readonly LocalizationService _localization;       
        private readonly IAuthenticationService _authenticationService;
        private readonly UserManager<Tradify.Data.Entities.Identity.User> userManager;
        private readonly IMemoryCache _cache;
        #endregion
        #region constructor
        public AuthenticationQueryHandler(LocalizationService localization,IAuthenticationService authenticationService,
            UserManager<Tradify.Data.Entities.Identity.User> userManager, IMemoryCache cache) : base(localization)
        {
            _localization = localization;       
            _authenticationService = authenticationService;
            this.userManager = userManager;    
            this._cache = cache;    
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
            var user = userManager.Users.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Id == request.Id); ;
            if (user == null) return BadRequest<string>(_localization.Get("UserIsNotFound"));
            if (user.OTP == request.OTP)
            {
                user.PhoneNumberConfirmed = true;
              await  userManager.UpdateAsync(user);
                return Success<string>(_localization.Get("ConfirmPhoneDone"));
            }
            else
            {
                return BadRequest<string>(_localization.Get("OTP_IsWrong"));
            }
        }
         
        public async Task<Response<LoginGoogleResult>> Handle(GetSignByGoogleResult request, CancellationToken cancellationToken)
        {
            if(!_cache.TryGetValue(request.requestId,out Response<LoginGoogleResult> data))
                {

                     return BadRequest<LoginGoogleResult>("Expired or invalid request");
            }
            _cache.Remove(request.requestId);

            return data;
                   

        }


        #endregion


    }
}

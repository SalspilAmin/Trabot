using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Authenticaiton.Commands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Helpers;
using Tradify.Service.AbstractsServices.AuthenticationServices;
using Tradify.Service.AbstractsServices.IdentityServices;
using Tradify.Service.Services.IdentityServices;

namespace Tradify.Core.Features.Authenticaiton.Commands.Handler
{
    public class AuthenticationCommandHandler : ResponseHandler
     , IRequestHandler<SignInCommand, Response<JwtAuthResult>>
    {
        #region Fields
        private readonly LocalizationService localization;
        private readonly IAuthenticationService authenticationService;
        private readonly IUserService userService;
        private readonly UserManager<Tradify.Data.Entities.Identity.User> userManager;
        private readonly SignInManager<Tradify.Data.Entities.Identity.User> signInManager;

        #endregion
        #region Constructor
        public AuthenticationCommandHandler(LocalizationService localization, IAuthenticationService authenticationService, IUserService userService, UserManager<Tradify.Data.Entities.Identity.User> userManager
            , SignInManager<Tradify.Data.Entities.Identity.User> signInManager) : base(localization)
        {
            this.localization = localization;
            this.authenticationService = authenticationService;
            this.userService = userService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }



        #endregion


        #region Methods

  
       public async Task<Response<JwtAuthResult>> Handle(SignInCommand request, CancellationToken cancellationToken)
        {
            //Check if user is Exist
            var chekuserPhone = userService.IsPhone(request.EmailOrPhone);
            var checkuserEmail = userService.IsEmail(request.EmailOrPhone);
            if (!checkuserEmail && !chekuserPhone)
                return BadRequest<JwtAuthResult>(localization.Get("Add_Correct_info"));
            var GetUserbyEmailorPhone = await userService.FindUserByEmailOrPhoneAsync(request.EmailOrPhone);


            if (GetUserbyEmailorPhone == null) return BadRequest<JwtAuthResult>(localization.Get("Add_Correct_info"));


            // try to sign in
            var signinResult = await signInManager.CheckPasswordSignInAsync(GetUserbyEmailorPhone, request.Password, false);

            // if Faild return password is worng
            if (!signinResult.Succeeded) return BadRequest<JwtAuthResult>(localization.Get("PasswordNotCorrect"));





            // confrim Email or phone
            if (checkuserEmail || chekuserPhone)
            {
                if (!GetUserbyEmailorPhone.EmailConfirmed) return BadRequest<JwtAuthResult>(localization.Get("EmailNotConfirmed"));
                if (!GetUserbyEmailorPhone.PhoneNumberConfirmed) return BadRequest<JwtAuthResult>(localization.Get("PhoneNumberNotConfirmed"));

            }

            //  return token

            var JwtAuthToken = await authenticationService.GetJWTTokenAsync(GetUserbyEmailorPhone);


            return Success<JwtAuthResult>(JwtAuthToken);
        }
        #endregion

    }
}

using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Authenticaiton.Commands.Models;
using Tradify.Core.Resources;
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
      ,IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>
        ,IRequestHandler<SendResetPasswordCommand,Response<string>>
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

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var jwttoken =  authenticationService.ReadJWTToken(request.Accesstoken);
            var UserIdAndExpireDate =await authenticationService.ValidateDetails(jwttoken, request.Accesstoken, request.RefreshToken);
            switch (UserIdAndExpireDate)
            {
               case ("AlgorithmIsWrong", null): return Unauthorized<JwtAuthResult>(localization.Get("AlgorithmIsWrong"));
                case ("TokenIsNotExpired", null): return Unauthorized<JwtAuthResult>(localization.Get("TokenIsNotExpired"));
                case ("RefreshTokenIsNotFound", null): return Unauthorized<JwtAuthResult>(localization.Get("RefreshTokenIsNotFound"));
                case ("RefreshTokenIsExpired", null): return Unauthorized<JwtAuthResult>(localization.Get("RefreshTokenIsExpired"));
            }
            var (userId, expireDate) = UserIdAndExpireDate;
            var user= await userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return NotFound<JwtAuthResult>();
            }
            var result = await authenticationService.RefreshToken(user, jwttoken, expireDate, request.RefreshToken);
            return Success<JwtAuthResult>(result);
        }

        public async Task<Response<string>> Handle(SendResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await authenticationService.SendResetPasswordAsync(request.EmailOrPhone);
            switch (result)
            {
                case "userNotFount":  return BadRequest<string>(localization.Get("UserIsNotFound"));
                case "Failed":      return BadRequest<string>(localization.Get("TryAgainInAnotherTime"));
               

            }
           return Success<string>(localization.Get("Success"));
        }


        #endregion

    }
}

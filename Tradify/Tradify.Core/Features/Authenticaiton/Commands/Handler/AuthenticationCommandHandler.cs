using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
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
      , IRequestHandler<RefreshTokenCommand, Response<JwtAuthResult>>
        , IRequestHandler<SendResetPasswordCommand, Response<string>>
        , IRequestHandler<ConfirmResetPasswordCommand, Response<string>>
        ,IRequestHandler<ResetPasswordCommand, Response<string>>
        ,IRequestHandler<LoginWithGoogleCommand,Response<string>>,
        IRequestHandler<BeginCoonectionWithGoogleCommand,Response<string>>          
    {
        #region Fields
        private readonly LocalizationService localization;
        private readonly IAuthenticationService authenticationService;
        private readonly IUserService userService;
        private readonly UserManager<Tradify.Data.Entities.Identity.User> userManager;
        private readonly SignInManager<Tradify.Data.Entities.Identity.User> signInManager;
        private readonly IMemoryCache _cache;

        #endregion
        #region Constructor
        public AuthenticationCommandHandler(LocalizationService localization, IAuthenticationService authenticationService, IUserService userService, UserManager<Tradify.Data.Entities.Identity.User> userManager
            , SignInManager<Tradify.Data.Entities.Identity.User> signInManager,IMemoryCache cache) : base(localization)

        {
            this.localization = localization;
            this.authenticationService = authenticationService;
            this.userService = userService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this._cache = cache;    
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

              // confrim Email or phone
            if (checkuserEmail || chekuserPhone)
            {
                if (!GetUserbyEmailorPhone.EmailConfirmed && !GetUserbyEmailorPhone.PhoneNumberConfirmed) return BadRequest<JwtAuthResult>(localization.Get("EmailOrPhoneNotConfirmed"));
               

            }
            // try to sign in



            var signinResult = await signInManager.CheckPasswordSignInAsync(GetUserbyEmailorPhone, request.Password, false);

            // if Faild return password is worng
            if (!signinResult.Succeeded) return BadRequest<JwtAuthResult>(localization.Get("PasswordNotCorrect"));





          

            //  return token

            var JwtAuthToken = await authenticationService.GetJWTTokenAsync(GetUserbyEmailorPhone);


            return Success<JwtAuthResult>(JwtAuthToken);
        }

        public async Task<Response<JwtAuthResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var jwttoken = authenticationService.ReadJWTToken(request.Accesstoken);
            var UserIdAndExpireDate = await authenticationService.ValidateDetails(jwttoken, request.Accesstoken, request.RefreshToken);
            switch (UserIdAndExpireDate)
            {
                case ("AlgorithmIsWrong", null): return Unauthorized<JwtAuthResult>(localization.Get("AlgorithmIsWrong"));
                case ("TokenIsNotExpired", null): return Unauthorized<JwtAuthResult>(localization.Get("TokenIsNotExpired"));
                case ("RefreshTokenIsNotFound", null): return Unauthorized<JwtAuthResult>(localization.Get("RefreshTokenIsNotFound"));
                case ("RefreshTokenIsExpired", null): return Unauthorized<JwtAuthResult>(localization.Get("RefreshTokenIsExpired"));
            }
            var (userId, expireDate) = UserIdAndExpireDate;
            var user = userManager.Users.Where(x => x.IsDeleted == false).FirstOrDefault(x => x.Id == int.Parse(userId)); ;
            if (user == null)
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
                case "UserNotFount": return BadRequest<string>(localization.Get("UserIsNotFound"));
                case "Failed": return BadRequest<string>(localization.Get("TryAgainInAnotherTime"));


            }
            return Success<string>(localization.Get("Success"));
        }

        public async Task<Response<string>> Handle(ConfirmResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await authenticationService.ConfrimResetPasswordAsync(request.EmailOrPhone, request.Code);

            switch (result)
            {
                case "UserNotFound": return BadRequest<string>(localization.Get("UserIsNotFound"));
                case "CodeIsWrong" :return BadRequest<string>(localization.Get("CodeIsWrong"));

            }
            return Success<string>(localization.Get("Success"));

            #endregion

        }

        public async Task<Response<string>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await authenticationService.ResetPasswordAsync(request.EmailOrPhone, request.Password);
            switch (result)
            {
                case "UserNotFound": return BadRequest<string>(localization.Get("UserIsNotFound"));
                case "Failed": return BadRequest<string>(localization.Get("TryAgainInAnotherTime"));
                case "Success": return Success<string>("Success");
                default: return BadRequest<string>(localization.Get("TryAgainInAnotherTime"));
            }
        }

        public async Task<Response<string>> Handle(LoginWithGoogleCommand request, CancellationToken cancellationToken)
        {
            var result   = await authenticationService.GoogleCallback(request.Code);
            var requestId = Guid.NewGuid().ToString();
            var chacheOptions = new MemoryCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(3)
            };

            Response<LoginGoogleResult> response;
            if (result.Item1 != null)
            { 
            response= Success(result.Item1);
            } 

            else
            {
                response = result.Item2 switch
                {
                    "UserIsExit" => BadRequest<LoginGoogleResult>(localization.Get("IsExist")),
                    "EmailINGoogleNotVerified" => BadRequest<LoginGoogleResult>(localization.Get("Google_email_not_verified")),
                    "ErrorWhenTryCreateUserByGoogle" => BadRequest<LoginGoogleResult>(localization.Get("ErrorWhenTryCreateUserByGoogle")),
                    _ => BadRequest<LoginGoogleResult>(localization.Get("TryAgainInAnotherTime"))

                };
            }
            _cache.Set(requestId, response);

            return Success<string>(requestId);

        }

        public async Task<Response<string>> Handle(BeginCoonectionWithGoogleCommand request, CancellationToken cancellationToken)
        {
            var result = await authenticationService.GoogleLogin();
            return Success<string>(result);
        }
    }
}

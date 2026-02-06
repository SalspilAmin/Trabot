
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Tls;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Helpers;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.AuthenticationServices;
using Tradify.Service.AbstractsServices.IdentityServices;
using Tradify.Service.AbstractsServices.WhatsappServices;


namespace Tradify.Service.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly UserManager<User> userManager;
        private readonly JwtSettings jwtSettings;  
        private readonly IRefreshTokenRepository refreshTokenRepository;
        private readonly IUserService userService;
        private readonly IEmailService emailService;
        private readonly IWatsappService watsappService;
        #endregion


        #region Constructor
        public AuthenticationService(UserManager<User> userManager,JwtSettings jwtSettings,IRefreshTokenRepository refresh,IUserService userService,
            IEmailService emailService,IWatsappService watsapp)
        {
            this.userManager = userManager; 
            this.jwtSettings = jwtSettings;
            this.refreshTokenRepository = refresh;  
            this.userService = userService;
            this.emailService = emailService;
            this.watsappService = watsapp;
        }
        #endregion
        
      
       
        #region Methods
         public async Task<JwtAuthResult> GetJWTTokenAsync(User user)
        {
            var (jwttoken,Stringtoken) = await GenerateJWTTokenAsync(user);
            var refreshtokenobject =  GetRefreshTokenObject(user.UserName);

            var UserRefreshToken = new UserRefreshToken()
            {
                UserId = user.Id,
                IsRevoked = false,
                IsActive = true,
                ExpiredTime = DateTime.UtcNow.AddDays(jwtSettings.RefreshTokenExpireDate),
                RefreshToken = refreshtokenobject.RefreshTokenString,
                Token = Stringtoken,
                JwtId = jwttoken.Id,
                AddedTime = DateTime.UtcNow
            };
            await refreshTokenRepository.AddAsync(UserRefreshToken);
            var Response =new JwtAuthResult();
            Response.AccessToken = Stringtoken;
            Response.refreshToken = refreshtokenobject;
            return Response;

         }
        private async Task<(JwtSecurityToken,string)>  GenerateJWTTokenAsync(User user) 
        {
            var claims = await GetClaimsAsync(user);
            var jwtToken = new JwtSecurityToken(
                jwtSettings.Issuer,
                jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),SecurityAlgorithms.HmacSha256Signature)

                );
            var Tokenstring = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            
           return (jwtToken,Tokenstring);


        }

        private RefreshToken GetRefreshTokenObject(string username)
        {

            var RefreshToken = new RefreshToken()
            {
                UserName = username,
                ExpireAt = DateTime.Now.AddDays(jwtSettings.RefreshTokenExpireDate),
                RefreshTokenString = GenerateRefreshToken()
            };

            return RefreshToken;
        }
        private string   GenerateRefreshToken()
        {
            var randomnumber = new byte[32];
            var randomNumberGenerator =  RandomNumberGenerator.Create();
               randomNumberGenerator.GetBytes(randomnumber);
            return Convert.ToBase64String(randomnumber);
        }
        public async Task<List<Claim>> GetClaimsAsync(User user)
        {
            var claims = new List<Claim>() 
              {
                new Claim(ClaimTypes.Name,user.UserName),
                 new Claim(ClaimTypes.Email,user.Email),
                new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
                new Claim(nameof(UserClaimModel.Id), user.Id.ToString())

             };
            var roles = await userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var userClaims = await userManager.GetClaimsAsync(user);
            claims.AddRange(userClaims);
            return claims;

        }

        public async Task<JwtAuthResult> RefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken)
        {
            var (newjwtToken, newtoken) = await GenerateJWTTokenAsync(user);
            var response = new JwtAuthResult();
            response.AccessToken = newtoken;
            var refreshTokenResponse = new RefreshToken();
            refreshTokenResponse.UserName= jwtToken.Claims.FirstOrDefault(c=>c.Type==nameof(ClaimTypes.Name)).Value;
            refreshTokenResponse.RefreshTokenString = refreshToken;
            refreshTokenResponse.ExpireAt=(DateTime)expiryDate;
            response.refreshToken = refreshTokenResponse;

            return response;

        }

        public JwtSecurityToken ReadJWTToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
  
            }
            var handler = new JwtSecurityTokenHandler();
            var response = handler.ReadJwtToken(accessToken);

            return response;    
        }

        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameter = new TokenValidationParameters()
            {
                ValidateIssuer = jwtSettings.ValidateIssuer,
                ValidateAudience = jwtSettings.ValidateAudience,
                ValidateLifetime = jwtSettings.ValidateLifeTime,
                ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret))



            };
            try
            {
                var validation = handler.ValidateToken(accessToken, parameter, out SecurityToken securityToken);

                if (validation == null)
                {
                    return "InvalidToken";
                }
                return "NotExpired";
            }
            catch (Exception ex) { 
            return ex.Message;  
            }
        }

        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            if(jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("AlgorithmIsWrong", null);
            }
            if (jwtToken.ValidTo > DateTime.UtcNow) return ("TokenIsNotExpired", null);


            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == (nameof(UserClaimModel.Id))).Value;
            var userRerechtoken = await refreshTokenRepository.GetTableNoTracking().FirstOrDefaultAsync(x => x.Token == accessToken &&
                                                                                         x.RefreshToken == refreshToken && x.UserId ==int.Parse(userId));
            if (userRerechtoken == null)
            {
                return ("RefreshTokenIsNotFound", null);
            }
            if (userRerechtoken.ExpiredTime < DateTime.UtcNow)
            {
                userRerechtoken.IsRevoked= true;
                userRerechtoken.IsActive = false;
                await refreshTokenRepository.UpdateAsync(userRerechtoken);
                return ("RefreshTokenIsExpired", null);
            }
            var expireDate = userRerechtoken.ExpiredTime;
            return (userId, expireDate);    
        }

        public async Task<string> ConfirmEmailAsync(int? userId, string? code)
        {
            if (userId == null || code == null) return "ErrorWhenConfirmEmail";
            var user = await userManager.FindByIdAsync(userId.ToString());
            if (user == null) return "NotFound";
            var result = await userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
                return "ErrorWhenConfirmEmail";
            return "Success";

        }

        public async Task<string> SendResetPasswordAsync(string EmailorPhone)
        {
            var user = await userService.FindUserByEmailOrPhoneAsync(EmailorPhone);
            if (user == null) return "UserNotFount";
            var emailorphone= EmailorPhone.Trim();
            var code = new Random().Next(100000,999999).ToString();
               user.Code = code;
            if (emailorphone.Contains('@'))
            {
               
              var result=await emailService.SendEmail(EmailorPhone, $"Your code To Reset Password;{code}","ResetPassword");
                
               await userManager.UpdateAsync(user);  
                return result;
            }
            var watsappresult = await watsappService.SendVerificationCodeAsync(EmailorPhone, code)  ;

            await userManager.UpdateAsync(user);
            if (watsappresult) return "Success";
            return "Failed";
        }

        public async Task<string> ConfrimResetPasswordAsync(string EmailorPhone,string Code)
        {
            // get user
            var user = await userService.FindUserByEmailOrPhoneAsync(EmailorPhone);
            if (user == null) return "UserNotFound";

            // check code
            if(user.Code ==Code) return "Success";

            return "CodeIsWrong";
        }



        #endregion



    }
}

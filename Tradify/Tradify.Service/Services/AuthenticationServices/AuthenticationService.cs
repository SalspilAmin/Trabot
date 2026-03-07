
using Google.Apis.Auth;
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
using System.Text.Json;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Helpers;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Infrastructure.Context;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.AuthenticationServices;
using Tradify.Service.AbstractsServices.IdentityServices;
using Tradify.Service.AbstractsServices.WhatsappServices;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;


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
        private readonly ApplicationDbContext applicationDbContext;
        private readonly OAuthSettings oauthSettings;
        #endregion


        #region Constructor
        public AuthenticationService(UserManager<User> userManager, JwtSettings jwtSettings, IRefreshTokenRepository refresh, IUserService userService,
            IEmailService emailService, IWatsappService watsapp, ApplicationDbContext applicationDbContext,OAuthSettings oAuthSettings)
        {
            this.userManager = userManager; 
            this.jwtSettings = jwtSettings;
            this.refreshTokenRepository = refresh;  
            this.userService = userService;
            this.emailService = emailService;
            this.watsappService = watsapp;
            this.applicationDbContext = applicationDbContext;   
            this.oauthSettings = oAuthSettings;
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

        public async Task<string> ResetPasswordAsync(string EmailorPhone, string Password)
        {
            using (var trans = await applicationDbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    //get user 
                    var user = await userService.FindUserByEmailOrPhoneAsync(EmailorPhone);
                    if (user == null)
                        return "UserNotFound";
                    await userManager.RemovePasswordAsync(user);
                    if (!await userManager.HasPasswordAsync(user))
                    {
                        await userManager.AddPasswordAsync(user, Password);
                    }
                    await trans.CommitAsync();
                    return "Success";


                }
                catch (Exception ex)
                {
                    await trans.RollbackAsync();
                    return "Failed";
                }
            }
            
        }

        #region Oauth mehtods
        public  Task<string> GoogleLogin()
        {
            var url = "https://accounts.google.com/o/oauth2/v2/auth" +
              $"?client_id={oauthSettings.ClientId}" +
              $"&redirect_uri={oauthSettings.CallBackMethodUrl}" +
              "&response_type=code" +
              "&scope=openid email profile";
            return Task.FromResult(oauthSettings.CallBackMethodUrl);

        }

        public async  Task<(LoginGoogleResult?,string?)> GoogleCallback(string code)
        {

            var clientId = oauthSettings.ClientId;
            var clientSecret = oauthSettings.ClientSecret;
        var redirectUri = oauthSettings.CallBackMethodUrl;

        var values = new Dictionary<string, string>
    {
        { "code", code },
        { "client_id", clientId },
        { "client_secret", clientSecret },
        { "redirect_uri", redirectUri },
        { "grant_type", "authorization_code" }
    };

        var client = new HttpClient();
        var response = await client.PostAsync(
            "https://oauth2.googleapis.com/token",
            new FormUrlEncodedContent(values));

        var json = await response.Content.ReadAsStringAsync();
            try
            {
                var tokenResponse = JsonSerializer.Deserialize<GoogleTokenResponse>(json);



            // هنا بقى عندك id_token
                var payload = await GoogleJsonWebSignature.ValidateAsync(tokenResponse.id_token);

              // نفس منطق إنشاء المستخدم
               var check = await userManager.FindByEmailAsync(payload.Email);
                if (check != null)
                {
                    return (null, "UserIsExit");
                }
                 if (!payload.EmailVerified) return (null, "EmailINGoogleNotVerified");
                var result = await userManager.CreateAsync(new User { Email = payload.Email, UserName = payload.Name, EmailConfirmed = true });
               
                    if(!result.Succeeded) return (null, "ErrorWhenTryCreateUserByGoogle");

                var user = await userManager.FindByEmailAsync(payload.Email);
                var AddRoleResult = await userManager.AddToRoleAsync(user, "User");
                if (!AddRoleResult.Succeeded)
                {
                    return (null,string.Join(",", AddRoleResult.Errors.Select(x => x.Description).ToList()));
                }



                var Googleresult = new LoginGoogleResult { UserId = user.Id, UserEmail = user.Email, JwtAuthResult = await GetJWTTokenAsync(user) };

                return (Googleresult, null);
            }
            catch (Exception )
            {
                throw ;
            }



        
        }
        #endregion

        #endregion



}
}

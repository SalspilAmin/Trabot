
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Helpers;
using Tradify.Infrastructure.AbstractsRepositories;
using Tradify.Service.AbstractsServices.AuthenticationServices;


namespace Tradify.Service.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
             private readonly UserManager<User> userManager;
             private readonly JwtSettings jwtSettings;  
        private readonly IRefreshTokenRepository refreshTokenRepository;
        #endregion


        #region Constructor
        public AuthenticationService(UserManager<User> userManager,JwtSettings jwtSettings,IRefreshTokenRepository refresh)
        {
            this.userManager = userManager; 
            this.jwtSettings = jwtSettings;
            this.refreshTokenRepository = refresh;  
        }
        #endregion
        
      
       
        #region Methods
         public async Task<JwtAuthResult> GetJWTToken(User user)
        {
            var (jwttoken,Stringtoken) = await GenerateJWTToken(user);
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
        private async Task<(JwtSecurityToken,string)>  GenerateJWTToken(User user) 
        {
            var claims = await GetClaims(user);
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
        public async Task<List<Claim>> GetClaims(User user)
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
        #endregion

       

    }
}

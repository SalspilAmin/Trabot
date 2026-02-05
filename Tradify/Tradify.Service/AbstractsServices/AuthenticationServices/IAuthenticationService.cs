using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Helpers;

namespace Tradify.Service.AbstractsServices.AuthenticationServices
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJWTTokenAsync(User user);
        public Task<JwtAuthResult> RefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);
        public JwtSecurityToken ReadJWTToken(string accessToken);
        public Task<string> ValidateToken(string accessToken);
        public  Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken);

        public Task<string> ConfirmEmailAsync(int? userId, string? code);
        public Task<string> SendResetPasswordAsync(string EmailorPhone);

    }
}

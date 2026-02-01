using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities.Identity;
using Tradify.Data.Helpers;

namespace Tradify.Service.AbstractsServices.AuthenticationServices
{
    public interface IAuthenticationService
    {
        public Task<JwtAuthResult> GetJWTTokenAsync(User user);
    }
}

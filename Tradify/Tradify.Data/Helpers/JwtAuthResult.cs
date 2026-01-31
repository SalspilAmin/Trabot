using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Helpers
{
    public class JwtAuthResult
    {
        public string AccessToken { get; set; }
        public RefreshToken refreshToken { get; set; }
    
    
    }
    public class RefreshToken
    {
        public string UserName { get; set; }
        public string RefreshTokenString { get; set; }
        public DateTime ExpireAt { get; set; }
    }

}

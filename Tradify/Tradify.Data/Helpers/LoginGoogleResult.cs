using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Helpers
{
    public class LoginGoogleResult
    {
        public int UserId { get; set; }

        public string UserEmail { get; set; }
        public JwtAuthResult JwtAuthResult { get; set; }            
    }
}

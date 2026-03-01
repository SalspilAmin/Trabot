using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Tradify.Data.Helpers
{
    public class OAuthSettings
    {
        public string ClientId { get; set; }
      
        public string ClientSecret { get; set; }
        public string CallBackMethodUrl { get; set; }
    }
}

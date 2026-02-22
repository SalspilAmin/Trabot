using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Tradify.Data.Helpers
{
    public class ClaimsStore
    {
        public static List<Claim> claims = new()
        {
            new Claim("Create Product","false"),
            new Claim("Edit Product","false"),
            new Claim("Delete Product","false"),
        };
    }
}

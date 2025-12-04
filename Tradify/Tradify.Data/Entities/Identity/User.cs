using EntityFrameworkCore.EncryptColumn.Attribute;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Helpers;

namespace Tradify.Data.Entities.Identity
{
    public  class User : IdentityUser<int>
    {

        public User() 
        {
            UserRefreshTokens = new HashSet<UserRefreshToken>();
        }
   
        public UserName FullName { get; set; }

        public string? Address { get; set; }
        public string? Country { get; set; }
        [EncryptColumn]
        public string? Code { get; set; }
       // [InverseProperty(nameof(UserRefreshToken.user))]
        public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }

    }
}

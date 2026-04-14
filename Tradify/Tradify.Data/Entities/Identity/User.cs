using EntityFrameworkCore.EncryptColumn.Attribute;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Entities.Chat;
using Tradify.Data.Helpers;

namespace Tradify.Data.Entities.Identity
{
    public  class User : IdentityUser<int>
    {

        public User() 
        {
            UserRefreshTokens = new HashSet<UserRefreshToken>();
        }
   

        public string? Address { get; set; }
        public  int? SellerId { get; set; }

        public bool IsDeleted { get; set; }= false;
        public string? Country { get; set; }
        [EncryptColumn]
        public string? Code { get; set; }
        public string? OTP {  get; set; }
        [InverseProperty(nameof(UserRefreshToken.user))]
        public virtual ICollection<UserRefreshToken>? UserRefreshTokens { get; set; }

        public virtual Sellers? Seller { get; set; }
     
        public virtual Cart Cart{ get; set; }

        public virtual ICollection<Orders> Orders { get; set; }
        public virtual ICollection<Reviews>? Reviews { get; set; }
        public virtual ICollection<Favorite>? Favorites { get; set; }
        public virtual ICollection<Payouts>? Payouts { get; set; }
        public virtual ICollection<Appointment>? Appointments { get; set; }

        public virtual ICollection<Message>? SentMessages { get; set; }
        public virtual ICollection<Message>? ReceiveMessages { get; set; }

    }
}

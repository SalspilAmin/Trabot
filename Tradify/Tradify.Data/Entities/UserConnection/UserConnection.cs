using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Entities.Identity;

namespace Tradify.Data.Entities.UserConnection
{
    public  class UserConnection
    {
        public int Id { get; set; }
        
        public int UserId { get; set; } 

        public string ConnectionId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }  
    }
}

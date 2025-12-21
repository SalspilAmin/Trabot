using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Entities.Identity;

namespace Tradify.Data.Entities
{
    public class Sellers 
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public int StoreId { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
       
    
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        [ForeignKey(nameof(StoreId))]
        public Stores? Store { get; set; }
        public virtual ICollection<Products>? Products { get; set; }

        public virtual ICollection<Categories>? Categories { get; set; }
        public virtual ICollection<Payouts> Payouts { get; set; }                   
    }
}

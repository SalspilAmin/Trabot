using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Entities.Identity;

namespace Tradify.Data.Entities
{
    public class Cart
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]

        public virtual User User { get; set; }

        public virtual List<CartProduct>? CartProducts { get; set; } 
        public virtual List<Orders>? Orders { get; set; }    
    }
}

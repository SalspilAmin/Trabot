using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Xml.Linq;

namespace Tradify.Data.Entities
{

    public class Stores
    {

        public int Id { get; set; }

        public int SellerId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public bool CreatedAt { get; set; }
        [ForeignKey(nameof(SellerId))]
        public Sellers? Seller { get; set; }

        public  virtual ICollection<Products>? Products { get; set; }
        public virtual ICollection<Categories>? Categories { get; set; }
        public StoreBooking StoreBooking { get; set; }

    }
}

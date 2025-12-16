using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;

namespace Tradify.Data.Entities
{




    public class Products
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; } 

        public double Discount { get; set; }

        public double FinalPrice => Price - (Price * (Discount / 100));


        public int StoreId { get; set; }

        public int CategoryId { get; set; } 

        public int? SuborderId { get; set; }

        public int NumberOfProductInStock { get; set; }
        public bool InStock => NumberOfProductInStock > 0;





        public ProductImage? productImage { get; set; }
        public ProductVideo? productVideo { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Categories? Category { get; set; }
        [ForeignKey(nameof(StoreId))]
        public Stores? Store { get; set; }
        [ForeignKey(nameof(SuborderId))]
        public SubOrders? SubOrder { get; set; }
        public virtual ICollection<Reviews>? Reviews { get; set; }
        
      
        public virtual ICollection<ProductVariants>? ProductVariants { get; set; }
        public virtual ICollection<ProductImage>? ProductImages { get; set; }

        public virtual ICollection<ProductVideo>? ProductVideos { get; set; }


    }
}

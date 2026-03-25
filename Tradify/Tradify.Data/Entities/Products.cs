using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Emit;
using System.Text;
using System.Xml.Linq;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Data.Entities
{
    public class Products
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int StoreId { get; set; }

        public int CategoryId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdateAt { get; set; }
        public bool IsDeleted { get; set; } = false;


        [ForeignKey(nameof(CategoryId))]
        public virtual Categories? Category { get; set; }
        [ForeignKey(nameof(StoreId))]
        public virtual Stores? Store { get; set; }
       
        public virtual ICollection<Reviews>? Reviews { get; set; }
        public virtual ICollection<Favorite>? Favorites { get; set; }

        public virtual ICollection<ProductVariants>? ProductVariants { get; set; }
        public virtual ICollection<ProductImage>? ProductImages { get; set; }

        public virtual ICollection<ProductVideo>? ProductVideos { get; set; }

   
    }
}

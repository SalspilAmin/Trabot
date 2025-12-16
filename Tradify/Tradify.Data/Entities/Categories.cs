using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tradify.Data.Entities
{
    public class Categories
    {
        
        public int Id { get; set; } 

        public string Name { get; set; }    

        public int? ParentCategoryId { get; set; }

       
        public virtual ICollection<Products>? Products { get; set; }
        [ForeignKey(nameof(ParentCategoryId))]
        public Categories? Parent { get; set; }
        public virtual ICollection<Categories>? Children { get; set; }

    }
}

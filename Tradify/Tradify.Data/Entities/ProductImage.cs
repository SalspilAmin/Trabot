using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Helpers;

namespace Tradify.Data.Entities
{
    public  class ProductImage : ProductMedia
    {
         public bool IsMain { get; set; } = false;
        public int SortOrder { get; set; }

        [ForeignKey(nameof(ProductId))]
        public virtual Products? Product { get; set; }
    
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Data.Entities
{
    public class CartProduct
    {
        public int Id { get; set; } // optional, can also use composite key
        public int? CartId { get; set; }
        [ForeignKey(nameof(CartId))]
        public virtual Cart? Cart { get; set; }

        public int? ProductVariantId { get; set; }
        [ForeignKey(nameof(ProductVariantId))]
        public virtual ProductVariants? ProductVariant { get; set; }
        public int Quantity { get; set; }


    }
}

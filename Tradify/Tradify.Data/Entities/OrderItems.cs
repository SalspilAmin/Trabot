using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Entities;

namespace Tradify.Data.Entities
{
    public class OrderItems
    {
        public int Id { get; set; }

        public int SuborderId { get; set; }
        public int ProductVariantId { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        // Navigation
        [ForeignKey(nameof(SuborderId))]
        public virtual SubOrders SubOrder { get; set; }

        [ForeignKey(nameof(ProductVariantId))]
        public virtual ProductVariants  ProductVariant { get; set; }
    }
}

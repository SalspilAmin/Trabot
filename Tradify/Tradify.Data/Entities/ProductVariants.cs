using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Data.Entities
{
    public class ProductVariants
    {
       
        public int Id { get; set; }
        public string Name { get; set; }    
        public int ProductId { get; set; }
        public decimal Price { get; set; }

        public decimal Discount { get; set; } = 0;
        public string? Color { get; set; }   
        public string? Size { get; set; }
       
        public string ProductVarintName => $"{Color ?? ""} {Size ?? ""}".Trim();

        public bool IsDeleted { get; set; }=false;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal FinalPrice { get; private set; }
        // public decimal FinalPrice => Price - (Price * (Discount / 100));

        // JSON string to store additional attributes for the variant
       // public string? MetaData { get; set; }
        public int NumberOfProductInStock { get; set; }
        public bool InStock => NumberOfProductInStock > 0;
        
        //public int? SuborderId { get; set; }
       // public virtual ICollection<ProductVariantImage>? ProductVariantImages { get; set; } = new List<ProductVariantImage>();
        public virtual ProductVariantImage? ProductVariantImage { get; set; }

        //[ForeignKey(nameof(SuborderId))]
        //public virtual SubOrders? SubOrder { get; set; }
        public virtual ICollection<OrderItems> OrderItems { get; set; }
        [ForeignKey(nameof(ProductId))]  
        public virtual Products? Product {  get; set; }


    }
}

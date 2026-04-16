using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.ProductVariant.Queries.Results
{
    public class GetProductVariantByIdResponse
    {
        public int Id { get; set; }
        
        public string Name { get; set; } 
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalPrice { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public bool InStock { get; set; }
        public int NumberOfProductInStock { get; set; }
        public bool IsDeleted { get; set; }

        public ProductVariantImageResponse? Image { get; set; }

       
    }

    public class ProductVariantImageResponse
    {
        public int Id { get; set; }
        public string MediaPath { get; set; }  
       
    }
  
}


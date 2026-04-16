using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Tradify.Core.Features.ProductVariant.Queries.Models;

namespace Tradify.Core.Features.ProductVariant.Queries.Results
{
    public class GetProductVariantByProductResponse 
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalPrice { get; set; }
        public bool InStock { get; set; }
        public ProductVariantImageResponse? Image { get; set; }
       
    }
    
}

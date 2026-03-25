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
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalPrice { get; set; }
        public string? ProductVarintName { get; set; }
        public bool InStock { get; set; }
        public string? MainImage { get; set; }
       
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.ProductVariant.Queries.Results
{
    public class GetProductVariantByIdResponse
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal FinalPrice { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
        public string? ProductVarintName { get; set; }
        public bool InStock { get; set; }
        public int NumberOfProductInStock { get; set; }
        public bool IsDeleted { get; set; }

        public Dictionary<string, string>? MetaData { get; set; }

        public ICollection<ProductVariantImageResponse>? Images { get; set; }
    }

    public class ProductVariantImageResponse
    {
        public int Id { get; set; }
        public string MediaPath { get; set; } = string.Empty;
        public bool IsMain { get; set; }
        public int SortOrder { get; set; }
    }
  
}


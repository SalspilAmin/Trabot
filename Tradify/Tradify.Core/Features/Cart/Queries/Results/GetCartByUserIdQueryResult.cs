using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Entities;

namespace Tradify.Core.Features.Cart.Queries.Results
{
    public class GetCartByUserIdQueryResult
    {
        public int CartId { get; set; }
        public int UserId { get; set; }

        public List<CartProductResult>? ProductsInCart { get; set; }

    }

    public class CartProductResult {

        public int Id { get; set; }         
        public int ProductVariantId { get; set; }
        
        public virtual ProductVariantResult ProductVariant { get; set; }
        public int Quantity { get; set; }
    }

    public class ProductVariantResult
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProductId { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal FinalPrice { get; set; }

        public string? Color { get; set; }

        public string? Size { get; set; }

        public string ProductVariantName { get; set; }

        public int NumberOfProductInStock { get; set; }

        public bool InStock { get; set; }

        public string? ImageUrl { get; set; }
    }

}

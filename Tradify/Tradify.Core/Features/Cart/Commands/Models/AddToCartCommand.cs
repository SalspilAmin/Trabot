using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Cart.Queries.Models;

namespace Tradify.Core.Features.Cart.Commands.Models
{
    public class AddToCartCommand : IRequest<Response<string>>
    {
        public int CartId { get; set; } 

        public ProductVariantRequestToCart ProductVariant {  get; set; }

        
    }
    public class ProductVariantRequestToCart
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }

        public decimal Discount { get; set; } = 0;
        public string? Color { get; set; }
        public string? Size { get; set; }

        public string ProductVarintName => $"{Color ?? ""} {Size ?? ""}".Trim();

   

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal FinalPrice { get; private set; }
    }

}

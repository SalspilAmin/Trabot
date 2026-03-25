using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.ProductVariant.Commands.Models
{
    public class AddProductVariantCommand : IRequest<Response<string>>
    {
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; } = 0;
        public string? Color { get; set; }
        public string? Size { get; set; }
        public int NumberOfProductInStock { get; set; }
        public Dictionary<string, string>? MetaData { get; set; }
    }
}

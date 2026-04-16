using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.ProductVariant.Commands.Models
{
    public class AddProductVarintWithImageCommand : IRequest<Response<string>>
    {
        // public int StoreId { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; } = 0;
        public string? Color { get; set; }
        public string? Size { get; set; }
        public int NumberOfProductInStock { get; set; }
        public IFormFile Image { get; set; }


    }
}

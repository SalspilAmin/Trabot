using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Product.Commands.Models
{
    public class UpdateProductCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
        public double Discount { get; set; } = 0;
        public bool InStock { get; set; }

        public int NumberOfProductInStock { get; set; }
    }
}

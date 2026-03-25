using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Product.Commands.Models
{
    public class AddProductCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }
        
    }
}

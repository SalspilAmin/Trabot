using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Product.Commands.Models
{
    public class UpdateProductCommand : IRequest<Response<string>>
    {
        public int ProductId { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        
    }
}

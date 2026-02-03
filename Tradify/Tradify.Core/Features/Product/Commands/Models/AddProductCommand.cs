using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Product.Commands.Models
{
    public class AddProductCommand : IRequest<Response<int>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public double Discount { get; set; }
        public int CategoryId { get; set; }
        public int StoreId { get; set; }
        public int NumberOfProductInStock { get; set; }
     
    }
}

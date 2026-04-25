using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Discount.Product.Comands.Models
{
    public class AddProductDiscountCommand : IRequest<Response<string>>
    {
        public int ProductId { get; set; }
        public decimal Discount { get; set; }
    }
}

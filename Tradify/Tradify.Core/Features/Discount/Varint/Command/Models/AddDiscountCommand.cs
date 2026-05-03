using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Discount.Varint.Command.Models
{
    public class AddDiscountCommand : IRequest<Response<string>>
    {
        public int VariantId { get; set; }
        public decimal Discount { get; set; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Discount.Varint.Comands.Models
{
    public class DeleteDiscountCommand : IRequest<Response<string>>
    {
        public int VariantId { get; set; }
    }
}

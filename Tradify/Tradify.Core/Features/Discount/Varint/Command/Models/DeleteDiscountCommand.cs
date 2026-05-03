using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Discount.Varint.Command.Models
{
    public class DeleteDiscountCommand : IRequest<Response<string>>
    {
        public int VariantId { get; set; }
    }
}

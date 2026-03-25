using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.ProductVariant.Commands.Models
{
    public class DeleteDiscountCommand : IRequest<Response<string>>
    {
        public int VariantId { get; set; }
    }
}

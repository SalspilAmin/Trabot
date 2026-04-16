using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.ProductVariant.Queries.Results;
using Tradify.Core.Features.Store.Queries.Results;

namespace Tradify.Core.Features.ProductVariant.Queries.Models
{
    public class GetAllVarintByProductListQuery : IRequest<List<GetProductVariantByProductResponse>>
    {
        public int ProductId { get; set; }

    }
}

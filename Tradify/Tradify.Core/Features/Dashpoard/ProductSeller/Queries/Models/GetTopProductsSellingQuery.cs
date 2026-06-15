using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Dashpoard.ProductSeller.Queries.Results;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Dashpoard.ProductSeller.Queries.Models
{
    public class GetTopProductsSellingQuery : IRequest<Response<PaginatedResult<TopProductSellingResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}

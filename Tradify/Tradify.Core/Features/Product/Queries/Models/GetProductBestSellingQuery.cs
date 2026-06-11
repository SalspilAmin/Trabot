using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Product.Queries.Models
{
    public class GetProductBestSellingQuery : IRequest<PaginatedResult<GetSellerProductPaginationReponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

    }
}

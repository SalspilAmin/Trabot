using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Recomndition.Queries.Models
{
    public class GetTopRatedProductsRecommendQuery
    : IRequest<PaginatedResult<GetSellerProductPaginationReponse>>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }

}

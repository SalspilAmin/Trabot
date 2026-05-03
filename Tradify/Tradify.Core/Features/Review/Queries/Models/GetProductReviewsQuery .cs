using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.ProductVariant.Queries.Results;
using Tradify.Core.Features.Review.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Review.Queries.Models
{
    public class GetProductReviewsQuery : IRequest<Response<PaginatedResult<ReviewsResponse>>>
    {
        public int ProductId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
       
    }
}

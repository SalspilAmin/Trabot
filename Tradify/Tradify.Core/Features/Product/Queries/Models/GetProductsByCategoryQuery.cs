using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Product.Queries.Models
{
    public class GetProductsByCategoryQuery : IRequest<PaginatedResult<GetProductByCategoryResponse>>
    {
        public int CategoryId { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetProductsByCategoryQuery(int categoryId, int page, int pageSize)
        {
            CategoryId = categoryId;
            PageNumber = page;
            PageSize = pageSize;
        }
    }
}


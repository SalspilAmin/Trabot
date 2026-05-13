using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Post.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Post.Queries.Models
{
    public class GetPostPaginationQuery : IRequest<Response<PaginatedResult<GetListOfPostsResult>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public GetPostPaginationQuery(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }
        }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Dashpoard.ProductSeller.Queries.Results;
using Tradify.Core.Features.Dashpoard.ServiceSeller.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Dashpoard.ServiceSeller.Queries.Models
{
    public class GetTopInstructorsQuery : IRequest<Response<PaginatedResult<TopInstructorResponse>>>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}

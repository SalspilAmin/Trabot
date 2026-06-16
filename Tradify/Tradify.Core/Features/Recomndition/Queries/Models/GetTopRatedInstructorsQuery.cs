using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Dashpoard.ServiceSeller.Queries.Results;
using Tradify.Core.Features.Recomndition.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Recomndition.Queries.Models
{
    public class GetTopRatedInstructorsQuery : IRequest<PaginatedResult<GetRecommendedInstructorsResponse>>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Instructor.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Instructor.Queries.Models
{
    public class GetInstructorWithDiscountQuery : IRequest<PaginatedResult<GetInstructorWithDiscountResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}

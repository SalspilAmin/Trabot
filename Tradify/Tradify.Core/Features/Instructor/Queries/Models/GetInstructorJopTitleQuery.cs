using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Categorie.Queries.Results;
using Tradify.Core.Features.Instructor.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Instructor.Queries.Models
{
    public class GetInstructorJopTitleQuery : IRequest<Response<PaginatedResult<GetInstructorJopTitleResponse>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? StoreId { get; set; }    
    
    }
}

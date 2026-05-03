using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Instructor.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Instructor.Queries.Models
{
    public class GetInstructorPagnitionQuery : IRequest<Response<PaginatedResult<GetInstructorPagnitionRespons>>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? StoreId { get; set; }
        public string? Search { get; set; }
        public string? JobTitle { get; set; }

        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? AvailableToday { get; set; }

        public int? MinYearsOfExperience { get; set; }

        public int? MinRating { get; set; }

    }
}

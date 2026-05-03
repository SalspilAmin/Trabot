using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Education.Queries.Results;
using Tradify.Core.Features.InstructorSchedules.Queries.Results;
using Tradify.Core.Features.Store.Queries.Results;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.InstructorSchedules.Queries.Models
{
    public class GetInstructorSchedulesQuery : IRequest<List<GetInstructorSchedulesResponse>>
    {
        public int Id { get; set; } //InstructorId
        public GetInstructorSchedulesQuery(int id)
        {
            Id = id;
        }
    }
}

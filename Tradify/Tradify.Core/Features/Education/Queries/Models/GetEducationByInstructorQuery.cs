using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Education.Queries.Results;
using Tradify.Core.Features.Store.Queries.Results;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.Education.Queries.Models
{
    public class GetEducationByInstructorQuery : IRequest<List<GetEducationByInstructorResponse>>
    {
        public int Id { get; set; } //InstructorId
        public GetEducationByInstructorQuery(int id)
        {
            Id = id;
        }
    }
}

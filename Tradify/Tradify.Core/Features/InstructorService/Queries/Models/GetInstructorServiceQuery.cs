using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Education.Queries.Results;
using Tradify.Core.Features.InstructorService.Queries.Results;
using Tradify.Core.Features.Store.Queries.Results;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.InstructorService.Queries.Models
{
    public class GetInstructorServiceQuery : IRequest<List<GetInstructorServiceResponse>>
    {
        public int Id { get; set; } //InstructorId
        public GetInstructorServiceQuery(int id)
        {
            Id = id;
        }
    }
}

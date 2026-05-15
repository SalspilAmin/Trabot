using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.InstructorSchedules.Queries.Results;

namespace Tradify.Core.Features.InstructorSchedules.Queries.Models
{
    public class GetNotAvilableInstructoreSchedulesQuery : IRequest<Response<List<GetNotAvilableInstructoreSchedulesResponse>>>
    {
       
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Dashpoard.Instructor.Queries.Results;

namespace Tradify.Core.Features.Dashpoard.Instructor.Queries.Models
{
    public class GetInstructorDashboardQuery :IRequest<Response<GetInstructorDashboardResponse>>

    {
    }
}

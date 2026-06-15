using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Dashpoard.Instructor.Queries.Models;
using Tradify.Core.Features.Dashpoard.ServiceSeller.Queries.Models;
using Tradify.Data.AppMetaData;
using Tradify.Data.Helpers;

namespace Tradify.Controllers.Dashpoard
{
    [Authorize(Roles = RolesHelper.Instructor)]
    public class InstructorDashpoardController : AppControllerBase
    {
        [HttpGet(Router.Dashpoard.Instructor)]
        public async Task<IActionResult> GetInstructorDashboard()
        {
            var result = await Mediator.Send(new GetInstructorDashboardQuery());
            return NewResult(result);
        }


        [HttpGet(Router.Dashpoard.InstructorSessionsChart)]
        public async Task<IActionResult> GetInstructorSessionsChart()
        {
            var result = await Mediator.Send(new GetInstructorSessionsChartQuery());
            return NewResult(result);
        }

        [HttpGet(Router.Dashpoard.UpcomingAppointments)]
        public async Task<IActionResult> GetUpcomingAppointments()
        {
            var result = await Mediator.Send(new GetUpcomingAppointmentsQuery());
            return NewResult(result);
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.InstructorSchedules.Command.Models
{
    public class AddInstructorSchedulesCommand : IRequest<Response<string>>
    {
        public DayOfWeek Day { get; set; }
        public string StartTime { get; set; } // "05:00"
        public string EndTime { get; set; }

        public int Capacity { get; set; }
    }
}

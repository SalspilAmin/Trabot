using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.InstructorSchedules.Queries.Results
{
    public class GetInstructorSchedulesResponse
    {
        public int Id { get; set; }

        public string Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public string Date { get; set; }
        public int Available { get; set; }
        public bool IsAvailable { get; set; }

    }
}

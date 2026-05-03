using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Education.Queries.Results
{
    public class GetEducationByInstructorResponse
    {
        public string Degree { get; set; }
        public string Institution { get; set; } // Name of School
        public int Year { get; set; }

    }
}

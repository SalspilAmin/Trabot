using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Store.Queries.Results;

namespace Tradify.Core.Features.Instructor.Queries.Results
{
    public class GetInstructorPagnitionRespons
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }

        public int YearsOfExperience { get; set; }
        public decimal PricePerSession { get; set; }
        public decimal FinalPrice { get; set; }
        public decimal Discount { get; set; }

        public double AverageRating { get; set; }
        public int ReviewsCount { get; set; }
        public bool AvailableToday { get; set; }

        public InstructorImageResponse? Image { get; set; }


    }
  
}

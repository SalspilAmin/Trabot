using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Instructor.Queries.Results
{
    public class GetInstructorByIdResponse 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string About { get; set; }

        public int YearsOfExperience { get; set; }

        public decimal PricePerSession { get; set; }
        public decimal FinalPrice { get; set; }
        public decimal Discount { get; set; }


        public bool AvailableToday { get; set; }

        public double AverageRating { get; set; }

        public int ReviewsCount { get; set; }
        public InstructorImageResponse? Image { get; set; }

    }
    public class InstructorImageResponse
    {
        public int Id { get; set; }

        public string MediaPath { get; set; }

    }
}

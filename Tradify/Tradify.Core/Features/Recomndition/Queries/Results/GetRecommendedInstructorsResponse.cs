using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Instructor.Queries.Results;

namespace Tradify.Core.Features.Recomndition.Queries.Results
{
    public class GetRecommendedInstructorsResponse 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }

        public int YearsOfExperience { get; set; }
        public decimal PricePerSession { get; set; }

        public decimal Discount { get; set; }

        public decimal FinalPrice { get; set; }



        public double AverageRating { get; set; }
        public int ReviewsCount { get; set; }

        public InstructorImageResponse? Image { get; set; }
    }
}

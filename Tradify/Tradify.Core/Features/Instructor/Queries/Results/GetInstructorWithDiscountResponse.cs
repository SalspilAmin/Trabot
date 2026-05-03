using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Instructor.Queries.Results
{
    public class GetInstructorWithDiscountResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public decimal PricePerSession { get; set; }
        public decimal FinalPrice { get; set; }
        public decimal Discount { get; set; } 

        public InstructorImageResponse? Image { get; set; }

    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Instructor.Command.Models
{
    public class AddInstructorCommand :IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string About { get; set; }
        public decimal Discount { get; set; } = 0;

        public int YearsOfExperience { get; set; }

        public decimal PricePerSession { get; set; }

    }
}

using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Instructor.Command.Models
{
    public class AddInstructorWithImageCommand : IRequest<Response<string>>
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string About { get; set; }

        public int YearsOfExperience { get; set; }

        public decimal PricePerSession { get; set; }
        public decimal Discount { get; set; } = 0;

        public IFormFile Image { get; set; }


    }
}

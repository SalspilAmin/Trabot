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
        public string EmailOrPhone { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string About { get; set; }
        public decimal Discount { get; set; } = 0;

        public int YearsOfExperience { get; set; }

        public decimal PricePerSession { get; set; }

        public IFormFile Image { get; set; }


    }
}

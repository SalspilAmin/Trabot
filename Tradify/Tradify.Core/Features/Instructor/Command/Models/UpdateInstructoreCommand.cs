using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Instructor.Command.Models
{
    public class UpdateInstructoreCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string About { get; set; }
        public decimal PricePerSession { get; set; }

        public int YearsOfExperience { get; set; }

    }
}

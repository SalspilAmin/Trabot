using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Education.Command.Models
{
    public class AddInstructorEducationCommand : IRequest<Response<string>>
    {
        public string Degree { get; set; }
        public string Institution { get; set; } // Name of School
        public int Year { get; set; }
    }
}

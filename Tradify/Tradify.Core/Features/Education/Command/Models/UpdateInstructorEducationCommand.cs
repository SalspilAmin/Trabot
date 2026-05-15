using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Education.Command.Models
{
    public class UpdateInstructorEducationCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Degree { get; set; }
        public string Institution { get; set; } 
        public int Year { get; set; }

    }
}

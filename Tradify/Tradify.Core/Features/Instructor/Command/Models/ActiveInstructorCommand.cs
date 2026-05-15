using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Instructor.Command.Models
{
    public class ActiveInstructorCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public ActiveInstructorCommand(int id)
        {
            Id = id;
        }
    }
}

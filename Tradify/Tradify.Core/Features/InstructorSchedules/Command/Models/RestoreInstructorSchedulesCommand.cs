using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.InstructorSchedules.Command.Models
{
    public class RestoreInstructorSchedulesCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public RestoreInstructorSchedulesCommand(int id)
        {
            Id = id;
        }
    }
}

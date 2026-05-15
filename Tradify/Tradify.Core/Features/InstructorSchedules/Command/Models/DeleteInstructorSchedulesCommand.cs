using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.InstructorSchedules.Command.Models
{
    public class DeleteInstructorSchedulesCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public DeleteInstructorSchedulesCommand(int id)
        {
            Id = id;
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Education.Command.Models
{
    public class DeleteInstructorEducationCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public DeleteInstructorEducationCommand(int id)
        {
            Id = id;
        }
    }
}

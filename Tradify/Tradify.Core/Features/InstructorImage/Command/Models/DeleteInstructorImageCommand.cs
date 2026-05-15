using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.InstructorImage.Command.Models
{
    public class DeleteInstructorImageCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteInstructorImageCommand(int id)
        {
            this.Id = id;
        }
    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Certification.Command.Models
{
    public class DeleteInstructorCertificationCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public DeleteInstructorCertificationCommand(int id)
        {
            Id = id;
        }
    }
}

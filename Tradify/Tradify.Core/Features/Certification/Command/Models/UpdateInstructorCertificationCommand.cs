using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Certification.Command.Models
{
    public class UpdateInstructorCertificationCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Title { get; set; }


    }
}

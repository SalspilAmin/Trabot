using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Certification.Command.Models
{
    public class AddInstructorCertificationCommand : IRequest<Response<string>>
    {
        public string Title { get; set; }

    }
}

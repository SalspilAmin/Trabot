using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.InstructorService.Command.Models
{
    public class AddInstructorServiceCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }

    }
}

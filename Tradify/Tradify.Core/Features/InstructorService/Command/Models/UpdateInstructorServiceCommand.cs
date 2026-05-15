using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.InstructorService.Command.Models
{
    public class UpdateInstructorServiceCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }


    }
}

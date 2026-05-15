using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.InstructorImage.Command.Models
{
    public class AddInstructorImageCommand : IRequest<Response<string>>
    {

        public int InstructorId { get; set; }
        public IFormFile Image { get; set; }


    }
}

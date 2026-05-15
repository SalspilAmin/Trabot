using MediatR;
using Microsoft.AspNetCore.Http;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.InstructorImage.Command.Models
{
    public class UpdateInstructorImageCommand : IRequest<Response<string>>
    {
        public int ImageId { get; set; }
        public IFormFile Image { get; set; }

    }
}

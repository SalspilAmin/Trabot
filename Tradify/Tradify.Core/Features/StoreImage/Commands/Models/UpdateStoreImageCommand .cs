using MediatR;
using Microsoft.AspNetCore.Http;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.StoreImage.Commands.Models
{
    public class UpdateStoreImageCommand : IRequest<Response<string>>
    {
        public int ImageId { get; set; }
        public IFormFile Image { get; set; }

    }

}

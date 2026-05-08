using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Post.Commands.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Post
{
    [ApiController]
    public class PostController : AppControllerBase
    {
        [HttpPost(Router.Post.AddPost)]
        public async Task<IActionResult> AddPost([FromForm] AddPostModelCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

    }
}

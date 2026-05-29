using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Post.Commands.Models;
using Tradify.Core.Features.Post.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Post
{

    public class PostController : AppControllerBase
    {
        [HttpPost(Router.Post.AddPost)]
        public async Task<IActionResult> AddPost([FromForm] AddPostModelCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
        [HttpGet(Router.Post.GetPostsOfUserByID)]
        public async Task<IActionResult> GetPostPost([FromRoute] int id)
        {
            var response = await Mediator.Send(new GetPostsOfUsersQueriy(id) );
            return NewResult(response);
        }
        [HttpGet(Router.Post.GetPostByID)]
        public async Task<IActionResult> GetPostByID([FromRoute] int id)
        {
            var response = await Mediator.Send(new GetPostByIdQuery(id));
            return NewResult(response);
        }
        [HttpGet(Router.Post.GetPosts)]
        public async Task<IActionResult> GetPosts([FromQuery] GetPostPaginationQuery command)
        {
            var response = await Mediator.Send(command);
             return Ok(response);
        }

    }
}

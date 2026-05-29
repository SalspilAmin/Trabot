using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Comments.Commands.Models;
using Tradify.Core.Features.Comments.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Comments
{
    
    [ApiController]
    public class CommentsController : AppControllerBase
    {
        [HttpPost(Router.Comments.Addcomment)]
        public async Task<ActionResult> AddComment([FromQuery] AddCommentCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }
        [HttpPut(Router.Comments.Updatecomment)]
        public async Task<ActionResult> UpdateComment([FromQuery] UpdateCommentCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }
        [HttpDelete(Router.Comments.Deletecomment)]
        public async Task<ActionResult> DeleteComment([FromQuery] DeleteCommentCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }





        [HttpPost(Router.ReplayOnComments.AddReplayComment)]
        public async Task<ActionResult> AddReplayComment([FromQuery] AddReplyCommentCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }
        [HttpPut(Router.ReplayOnComments.UpdateReplayComment)]
        public async Task<ActionResult> UpdateReplayComment([FromQuery] UpdateReplyCommentCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }
        [HttpDelete(Router.ReplayOnComments.DeleteReplayComment)]
        public async Task<ActionResult> DeleteReplayComment([FromQuery] DeleteReplyCommentCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }
        [HttpGet(Router.Comments.GetCommentsByPostId)]
        public async Task<ActionResult> GetCommentsByPostId(
    [FromQuery] GetCommentsByPostIdQuery request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }

        [HttpGet(Router.Comments.GetCommentsByCommentId)]
        public async Task<ActionResult> GetCommentById(
            [FromQuery] GetCommentByIdQuery request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }


        [HttpGet(Router.ReplayOnComments.GetReplayesByCommentId)]
        public async Task<ActionResult> GetRepliesByCommentId(
            [FromQuery] GetRepliesByCommentIdQuery request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }

        [HttpGet(Router.ReplayOnComments.Get)]
        public async Task<ActionResult> GetReplyById(
            [FromQuery] GetReplyByIdQuery request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }

    }
}

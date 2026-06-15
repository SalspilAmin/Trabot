using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Messages.Commands.Models;
using Tradify.Core.Features.Messages.Queries.Models;
using Tradify.Data.AppMetaData;
using Tradify.Data.Entities.Identity;

namespace Tradify.Controllers.Message
{
   
    [ApiController]
    public class MessagesController : AppControllerBase
    {
        [HttpPost(Router.Message.AddMessage)]
        public async Task<IActionResult> AddMessage([FromForm] AddMessageCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }
        [HttpDelete(Router.Message.Delete)]
        public async Task<IActionResult> DeleteMessage([FromForm] DeleteMessageCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }
        [HttpPut(Router.Message.Update)]
        public async Task<IActionResult> UpdateMessage([FromForm] UpdateMessageCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }
        [HttpPatch(Router.Message.IsRead)]
        public async Task<IActionResult> ReadMessage([FromForm] MarkMessageAsReadCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }
        [HttpGet(Router.Message.Conversation)]
        public async Task<IActionResult> Conversation([FromQuery] GetConversationQuery request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }
        [HttpGet(Router.Message.UnReadMessages)]
        public async  Task<IActionResult> UnRead([FromRoute] int id)
        {
         var request =   new GetUnreadMessagesQuery() { UserId = id };
        var result = await Mediator.Send(request);
            return NewResult(result);
        }
        [HttpGet(Router.Message.GetById)]
        public async Task<IActionResult> GetMessage([FromRoute] int id)
        {
           
            var result = await Mediator.Send(new GetMessageByIdQuery(){MessageId = id});
            ;
            return NewResult(result);
        }

    }
}

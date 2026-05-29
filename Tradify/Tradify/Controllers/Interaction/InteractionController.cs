using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Interactions.Commands.Models;
using Tradify.Core.Features.Interactions.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Interaction
{
    
    [ApiController]
    public class InteractionController : AppControllerBase
    {
        [HttpPost(Router.Interaction.AddInteraction)]

        public async Task<IActionResult> Add(AddInteractionWithPostCommand command)
        {

            var result=  await Mediator.Send(command);
            return NewResult(result);
        }
        [HttpPut(Router.Interaction.UpdateInteraction)]
        public async Task<IActionResult> Update(
        UpdateInteractionWithPostCommand command)
        {
           
            var result =    await Mediator.Send(command);
            return NewResult(result);
        }

        [HttpDelete(Router.Interaction.DeleteInteraction)]
        public async Task<IActionResult> Delete(
        int id)
        {
            
             var result =   await Mediator.Send(
                    new DeleteInteractionWithPostCommand
                    {
                        InteractionId = id
                    });
            return NewResult(result);   
        }
        [HttpGet(Router.Interaction.GetInteractionByPostId)]
        public async Task<IActionResult>GetByPostId(int id)
        {
             var result = await Mediator.Send(
                     new GetInteractionsByPostIdQuery
                    {
                        PostId = id
                    });
            return NewResult(result);   
        }

        [HttpGet(Router.Interaction.GetInteractionById)]
        public async Task<IActionResult>GetById(int id)
        {
            var result = await Mediator.Send(
                    new GetInteractionByIdQuery
                    {
                        InteractionId = id
                    });
            return NewResult(result);
        }

        [HttpGet(Router.Interaction.GetUserInteractionOnPost)]
        public async Task<IActionResult> GetUserInteraction( [FromQuery] int userId,[FromQuery] int postId)
        {
           
         var  result =   await Mediator.Send(
                    new GetUserInteractionOnPostQuery
                    { UserId = userId,PostId = postId });
            return NewResult(result);
        }
    }
}

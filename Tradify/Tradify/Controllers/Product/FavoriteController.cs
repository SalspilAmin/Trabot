using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Favorites.Commands.Models;
using Tradify.Core.Features.Favorites.Queries.Models;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.Review.Commands.Models;
using Tradify.Core.Features.User.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers.Product
{
    [ApiController]
    public class FavoriteController : AppControllerBase
    {
        //[HttpPost(Router.Favorite.Add)]
        //public async Task<IActionResult> Add([FromForm] AddFavoriteCommand command)
        //{
        //    var response = await Mediator.Send(command);
        //    return NewResult(response);
        //}
        //[HttpDelete(Router.Favorite.Delete)]
        //public async Task<IActionResult> DeleteFavorite([FromRoute] int FavoriteId)
        //{
        //    var response = await Mediator.Send(new DeleteFavoriteCommand(FavoriteId));
        //    return NewResult(response);
        //}

        [HttpDelete(Router.Favorite.Toggle)]
        public async Task<IActionResult> ToggleFavorite([FromBody] ToggleFavoriteCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }

        [HttpGet(Router.Favorite.Paginated)]
        public async Task<IActionResult> GetPagination([FromQuery] GetUserFavoritesQuery request)
        {
            var result = await Mediator.Send(request);
            return Ok(result);
        }

    }
}

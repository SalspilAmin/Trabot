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
    

        [HttpDelete(Router.Favorite.Toggle)]
        public async Task<IActionResult> ToggleFavorite([FromForm] ToggleFavoriteCommand command)
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

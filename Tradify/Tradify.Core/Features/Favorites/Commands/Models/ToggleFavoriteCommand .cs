using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Favorites.Queries.Results;

namespace Tradify.Core.Features.Favorites.Commands.Models
{
    public class ToggleFavoriteCommand : IRequest<Response<ToggleFavoriteResponse>>
    {
        public int ProductId { get; set; }
    }
}

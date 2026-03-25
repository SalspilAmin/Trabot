using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Favorites.Commands.Models
{
    public class AddFavoriteCommand : IRequest<Response<string>>
    {
        public int ProductId { get; set; }
    }
}

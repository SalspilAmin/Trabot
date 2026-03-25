using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Favorites.Commands.Models
{
    public class DeleteFavoriteCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteFavoriteCommand(int Id)
        {
            this.Id = Id;
        }
    }
}

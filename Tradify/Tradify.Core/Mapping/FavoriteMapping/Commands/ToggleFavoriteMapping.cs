using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Favorites.Commands.Models;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.FavoriteMapping
{
    public partial class FavoriteProfile
    {
        public void ToggleFavoriteMapping()
        {
            CreateMap<ToggleFavoriteCommand, Favorite>();
        }

    }
    
}

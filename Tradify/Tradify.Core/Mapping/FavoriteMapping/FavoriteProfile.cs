using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.FavoriteMapping
{
    public partial class FavoriteProfile : Profile
    {
        public FavoriteProfile()
        {
            AddFavoriteMapping();
            ToggleFavoriteMapping();
        }
    }
    
    
}

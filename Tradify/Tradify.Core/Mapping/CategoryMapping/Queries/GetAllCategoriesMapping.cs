using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Categorie.Queries.Results;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.CategoryMapping
{
    public partial class CategoriesProfile
    {
        public void GetAllCategoriesMapping()
        {
            CreateMap<Categories, GetAllCategoriesResponse>();
        }
    }
}

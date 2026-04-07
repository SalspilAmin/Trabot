using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Categorie.Queries.Results
{
    public class GetAllCategoriesResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? ParentCategoryId { get; set; }
    }
}

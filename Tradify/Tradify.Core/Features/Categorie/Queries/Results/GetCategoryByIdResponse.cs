using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Categorie.Queries.Results
{
    public class GetCategoryByIdResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public ParentCategoryDto? Parent { get; set; }

        public List<ChildCategoryDto>? Children { get; set; }
    }
    public class ParentCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
    public class ChildCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}

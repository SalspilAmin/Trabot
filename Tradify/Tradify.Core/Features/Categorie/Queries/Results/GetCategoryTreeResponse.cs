using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Categorie.Queries.Results
{
    public class GetCategoryTreeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<GetCategoryTreeResponse> Children { get; set; } = new List<GetCategoryTreeResponse>();
    }
}

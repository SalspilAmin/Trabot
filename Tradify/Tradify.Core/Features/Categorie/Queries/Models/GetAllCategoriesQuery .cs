using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Categorie.Queries.Results;
using Tradify.Core.Features.Product.Queries.Results;
using Tradify.Core.Wrappers;

namespace Tradify.Core.Features.Categorie.Queries.Models
{
    public class GetAllCategoriesQuery : IRequest<PaginatedResult<GetAllCategoriesResponse>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int? StoreId { get; set; }
        public int? ParentCategoryId { get; set; }

        // Optional filter: root categories only
        public bool OnlyRoot { get; set; } = false;
        public string? Search { get; set; }
        public bool? IsDeleted { get; set; }

    }
}

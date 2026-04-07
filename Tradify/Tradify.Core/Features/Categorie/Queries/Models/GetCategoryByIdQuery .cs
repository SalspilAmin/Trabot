using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Categorie.Queries.Results;

namespace Tradify.Core.Features.Categorie.Queries.Models
{
    public class GetCategoryByIdQuery : IRequest<Response<GetCategoryByIdResponse>>
    {
        public int Id { get; set; }

        public bool IncludeChildren { get; set; } = false;


    }
}


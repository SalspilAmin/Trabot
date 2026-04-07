using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Categorie.Queries.Results;

namespace Tradify.Core.Features.Categorie.Queries.Models
{
    public class GetCategoryTreeQuery : IRequest<Response<List<GetCategoryTreeResponse>>>
    {
    }
}

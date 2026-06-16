using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Recomndition.Queries.Results;

namespace Tradify.Core.Features.Recomndition.Queries.Models
{
    public class GetRecommendedInstructorsQuery : IRequest<List<GetRecommendedInstructorsResponse>>
    {

    }
}

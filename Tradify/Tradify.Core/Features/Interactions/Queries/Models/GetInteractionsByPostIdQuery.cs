using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Helpers.Results;

namespace Tradify.Core.Features.Interactions.Queries.Models
{

    public class GetInteractionsByPostIdQuery :
        IRequest<Response<List<InteractionWithPostResult>>>
    {
        public int PostId { get; set; }
    }
}

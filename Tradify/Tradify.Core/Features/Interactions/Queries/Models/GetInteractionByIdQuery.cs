using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Helpers.Results;

namespace Tradify.Core.Features.Interactions.Queries.Models
{
    public class GetInteractionByIdQuery :
     IRequest<Response<InteractionWithPostResult>>
    {
        public int InteractionId { get; set; }
    }
}

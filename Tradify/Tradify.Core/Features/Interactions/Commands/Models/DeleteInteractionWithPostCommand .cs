using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Interactions.Commands.Models
{
    public class DeleteInteractionWithPostCommand :
    IRequest<Response<string>>
    {
        public int InteractionId { get; set; }
    }
}

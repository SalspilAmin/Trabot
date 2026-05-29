using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.Interactions.Commands.Models
{
    public class UpdateInteractionWithPostCommand :
     IRequest<Response<string>>
    {
        public int InteractionId { get; set; }

        public InteractionType InteractionBy { get; set; }
    }
}

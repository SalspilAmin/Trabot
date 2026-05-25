using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.Interactions.Commands.Models
{
    public class AddInteractionWithPostCommand :
        IRequest<Response<int?>>
    {
        public int UserId { get; set; }

        public int PostId { get; set; }

        public InteractionType InteractionBy { get; set; }
    }
}

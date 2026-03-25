using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.Review.Commands.Models
{
    public class AddReviewCommand : IRequest<Response<string>>
    {
        public int ProductId { get; set; }

        public byte Rating { get; set; }
        public string? Comment { get; set; }
    
    }
}

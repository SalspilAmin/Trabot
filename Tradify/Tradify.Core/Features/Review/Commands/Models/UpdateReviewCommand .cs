using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Review.Commands.Models
{
    public class UpdateReviewCommand : IRequest<Response<string>>
    {
        public int Id { get; set; } 
        public byte Rating { get; set; }
        public string? Comment { get; set; }
    }
}

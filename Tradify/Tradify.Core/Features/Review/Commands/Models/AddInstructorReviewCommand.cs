using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.Review.Commands.Models
{
    public class AddInstructorReviewCommand : IRequest<Response<string>>
    {
        public int InstructorId { get; set; }

        public RatingValue Rating { get; set; }
        public string? Comment { get; set; }
    }
}

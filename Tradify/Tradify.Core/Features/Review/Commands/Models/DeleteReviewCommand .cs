using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Review.Commands.Models
{
    public class DeleteReviewCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public DeleteReviewCommand(int id) 
        {
            this.Id = id;
        }
    }
}

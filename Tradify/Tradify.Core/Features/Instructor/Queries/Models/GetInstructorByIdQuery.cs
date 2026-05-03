using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Instructor.Queries.Results;
using Tradify.Core.Features.Product.Queries.Results;

namespace Tradify.Core.Features.Instructor.Queries.Models
{
    public class GetInstructorByIdQuery : IRequest<Response<GetInstructorByIdResponse>>
    {

        public int Id { get; set; }
        public GetInstructorByIdQuery(int id)
        {
            Id = id;
        }
    }
}

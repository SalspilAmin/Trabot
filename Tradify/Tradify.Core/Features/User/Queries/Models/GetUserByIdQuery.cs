using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.User.Queries.Results;

namespace Tradify.Core.Features.User.Queries.Models
{
    public class GetUserByIdQuery :  IRequest<Response<GetUserByIdResponse>>
    {

        public int Id { get; set; }
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }

    }
}

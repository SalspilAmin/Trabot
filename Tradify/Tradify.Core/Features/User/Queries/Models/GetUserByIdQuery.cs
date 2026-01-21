using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.User.Queries.Models
{
    public class GetUserByIdQuery :  IRequest<Response<GetUserByIdQuery>>
    {

        public int Id { get; set; }
        public GetUserByIdQuery(int id)
        {
            Id = id;
        }

    }
}

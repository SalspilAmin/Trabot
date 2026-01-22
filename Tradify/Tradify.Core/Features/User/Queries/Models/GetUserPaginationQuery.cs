using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.User.Queries.Models
{
    public class GetUserPaginationQuery : IRequest<Response<GetUserPaginationQuery>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}

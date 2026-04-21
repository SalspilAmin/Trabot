using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.User.Queries.Results;
using Tradify.Data.Helpers.Results;

namespace Tradify.Core.Features.User.Queries.Models
{
    public class GetUserByTokenQuery : IRequest<Response<UserInfoFromToken>>
    {
        public string AccessToken { get; set; }
        public GetUserByTokenQuery(string AccessToken) { 
        this.AccessToken = AccessToken;
        }


    }
}

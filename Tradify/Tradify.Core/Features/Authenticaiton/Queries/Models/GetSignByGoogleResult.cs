using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Helpers;

namespace Tradify.Core.Features.Authenticaiton.Queries.Models
{
    public  class GetSignByGoogleResult : IRequest<Response<LoginGoogleResult>>
    {
        public string requestId;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.User.Queries.Results
{
    public class GetUserByIdResponse
    {
        public string UserName { get; set; }
        public string? Email { get; set; }

        public string? Phone {  get; set; }
    }
}

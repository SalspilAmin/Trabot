using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;

namespace Tradify.Data.Helpers.Results
{
    public class UserInfoFromToken
    {
        public string UserId { get; set; }
        public string? Email { get; set; }
        public string? Phone {  get; set; }
        public string UserName { get; set; }
        public int? CartId { get; set; }
        public List<string> Roles { get; set; }
    }
}

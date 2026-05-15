using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Seller.Queries.Results
{
    public class GetSellerByIdResponse
    {
        public int Id { get; set; }

        public string BusinessName { get; set; }
        public string BusinessType { get; set; }

        public bool IsActive { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string? StoreName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Seller.Queries.Results
{
    public class GetAllSellerResponse
    {
        public int Id { get; set; }

        public string BusinessName { get; set; }
        public string BusinessType { get; set; }

        public bool IsActive { get; set; }
    }
}

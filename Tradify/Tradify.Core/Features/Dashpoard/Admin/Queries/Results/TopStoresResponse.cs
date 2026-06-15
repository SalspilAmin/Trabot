using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Store.Queries.Results;

namespace Tradify.Core.Features.Dashpoard.Admin.Queries.Results
{
    public class TopStoresResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public StoreImageResponse Image { get; set; }
        public string Type { get; set; }
        public int TotalCount { get; set; }

    }
}

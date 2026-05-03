using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Store.Queries.Results
{
    public class GetAllStoresResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public StoreImageResponse Image { get; set; }
        public string Type { get; set; }


    }
}

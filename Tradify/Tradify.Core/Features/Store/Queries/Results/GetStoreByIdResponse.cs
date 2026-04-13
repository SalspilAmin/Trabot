using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Features.Store.Queries.Results
{
    public class GetStoreByIdResponse
    {

        public string Name { get; set; }
        public string BusinessName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public StoreImageResponse Image { get; set; }



    }
    public class StoreImageResponse
    {
        public int Id { get; set; }

        public string MediaPath { get; set; }

    }
}

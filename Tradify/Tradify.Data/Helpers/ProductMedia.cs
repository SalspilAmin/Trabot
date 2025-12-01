using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Helpers
{
    public class ProductMedia
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public string MediaPath { get; set; }
    }
}

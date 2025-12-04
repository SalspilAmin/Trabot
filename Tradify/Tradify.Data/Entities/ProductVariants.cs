using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Entities
{
    public class ProductVariants
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public List<string> Colors { get; set; }

        public string MetaData { get; set; }    


    }
}

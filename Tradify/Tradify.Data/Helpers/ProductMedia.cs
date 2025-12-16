using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Tradify.Data.Helpers
{
    [NotMapped]
    public class ProductMedia
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string MediaPath { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Data.Helpers;

namespace Tradify.Data.Entities
{
    public  class ProductVideo : ProductMedia
    {

        [ForeignKey(nameof(ProductId))]
        public Products? Product {  get; set; }
    }
}

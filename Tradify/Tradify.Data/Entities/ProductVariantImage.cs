using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Data.Entities
{
    public class ProductVariantImage
    {
        public int Id { get; set; }
        public string MediaPath { get; set; }
        public bool IsMain { get; set; } = false;
        public int SortOrder { get; set; }
        public int ProductVariantId { get; set; }


        [ForeignKey(nameof(ProductVariantId))]
        public virtual ProductVariants ProductVariant  { get; set; }
    }
}

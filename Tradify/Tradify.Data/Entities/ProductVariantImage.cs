using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using static Tradify.Data.AppMetaData.Router;

namespace Tradify.Data.Entities
{
    public class ProductVariantImage
    {
        public int Id { get; set; }
        public string MediaPath { get; set; }
        public int? ProductVariantId { get; set; }
        public string PublicId { get; set; } // For Update At Cloudinary


        [ForeignKey(nameof(ProductVariantId))]
        [JsonIgnore]
        public virtual ProductVariants ProductVariant  { get; set; }
    }
}

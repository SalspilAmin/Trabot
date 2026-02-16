using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tradify.Data.Helpers.Fawaterak.Einvoice
{
    public class CartItemModel
    {
        [JsonProperty("name")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Item price per unit
        /// </summary>
        [JsonProperty("price")]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        /// <summary>
        /// Quantity of this item
        /// </summary>
        [JsonProperty("quantity")]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }
}

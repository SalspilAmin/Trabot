using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Helpers.Fawaterak
{
    public class ProductINRequestApi
    {
        [JsonProperty("name")]
        public string name { get; set; }
        
            
        [JsonProperty("price")]
        public decimal price { get; set; }
        [JsonProperty("quantity")]
        public int? quantity { get; set; }
    }
}

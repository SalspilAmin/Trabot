using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Helpers.Fawaterak
{
    public class PaymentMethodsResponse
    {

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public List<PaymentMethod> Data { get; set; }
    }
}

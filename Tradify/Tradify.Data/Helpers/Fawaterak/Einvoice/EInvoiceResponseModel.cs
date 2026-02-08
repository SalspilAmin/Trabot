using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Helpers.Fawaterak.Einvoice
{
    public class EInvoiceResponseModel
    {
        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("data")]
        public EInvoiceResponseDataModel Data { get; set; }

    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Helpers.Fawaterak.Einvoice
{
    public class EInvoiceResponseDataModel
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        /// <summary>
        /// Unique invoice ID from Fawaterak
        /// </summary>
        [JsonProperty("invoiceId")]
        public string InvoiceId { get; set; }

        /// <summary>
        /// Invoice key for verification
        /// </summary>
        [JsonProperty("invoiceKey")]
        public string InvoiceKey { get; set; }
    }
}

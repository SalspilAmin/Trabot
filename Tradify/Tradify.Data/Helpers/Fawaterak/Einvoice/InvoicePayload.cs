using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Helpers.Fawaterak.Einvoice
{
    public class InvoicePayload
    {
        /// <summary>
        /// Your internal order ID
        /// </summary>
        /// 
        [JsonProperty("merchant_reference")]
        public string OrderId { get; set; }
    }
}

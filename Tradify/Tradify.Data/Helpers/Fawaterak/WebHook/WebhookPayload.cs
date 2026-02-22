using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Tradify.Data.Helpers.Fawaterak.WebHook
{
     public class WebhookPayload
    {
        [JsonPropertyName("OrderId")]
        public string? OrderId { get; set; }
    }
}

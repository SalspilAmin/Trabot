using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Tradify.Data.Helpers.Fawaterak.WebHook
{
    public class WebHookModel
    {
        /// <summary>
        /// Invoice ID from Fawaterak
        /// </summary>
        [JsonPropertyName("invoice_id")]
        [Required]
        public long InvoiceId { get; set; }

        /// <summary>
        /// Invoice key from Fawaterak
        /// </summary>
        [JsonPropertyName("invoice_key")]
        [Required]
        public string InvoiceKey { get; set; }

        /// <summary>
        /// Verification hash key
        /// </summary>
        [JsonPropertyName("hashKey")]
        [Required]
        public string HashKey { get; set; }

        /// <summary>
        /// Payment method used for the transaction
        /// </summary>
        [JsonPropertyName("payment_method")]
        [Required]
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Current status of the invoice
        /// </summary>
        [JsonPropertyName("invoice_status")]
        [Required]
        public string InvoiceStatus { get; set; }

        /// <summary>
        /// Payload as JSON string
        /// </summary>
        [JsonPropertyName("pay_load")]
        public string? PayloadString { get; set; }

        /// <summary>
        /// Parsed payload data
        /// </summary>
        //[JsonPropertyName("pay_load")]
        public WebhookPayload? Payload { get; set; }
    }
}

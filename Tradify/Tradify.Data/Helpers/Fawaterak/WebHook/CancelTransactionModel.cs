using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace Tradify.Data.Helpers.Fawaterak.WebHook
{
    public class CancelTransactionModel
    {
        [JsonPropertyName("hashKey")]
        [Required]
        public string HashKey { get; set; }

        /// <summary>
        /// Transaction reference ID
        /// </summary>
        [JsonPropertyName("referenceId")]
        [Required]
        public string ReferenceId { get; set; }

        /// <summary>
        /// Transaction status
        /// </summary>
        [JsonPropertyName("status")]
        [Required]
        public string Status { get; set; }

        /// <summary>
        /// Payment method used for the transaction
        /// </summary>
        [JsonPropertyName("paymentMethod")]
        [Required]
        public string PaymentMethod { get; set; }

        /// <summary>
        /// Additional payload data
        /// </summary>
        [JsonPropertyName("pay_load")]
        public PayloadModel? PayLoad { get; set; }

        public class PayloadModel
        {
            [JsonPropertyName("merchant_reference")]
            public string Merchant_Reference { get; set; }
            [JsonPropertyName("trasaction_data")]
            public string Trasaction_Data { get; set; }

        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Helpers.Fawaterak
{
    public class PaymentMethod
    {
        public int Id { get; set; }

        /// <summary>
        /// Fawaterak payment method ID
        /// </summary>
        [JsonProperty("paymentId")]
        public int PaymentId { get; set; }

        /// <summary>
        /// Payment method name in English
        /// </summary>
        [JsonProperty("name_en")]
        public string NameEn { get; set; }

        /// <summary>
        /// Payment method name in Arabic
        /// </summary>
        [JsonProperty("name_ar")]
        public string NameAr { get; set; }

        /// <summary>
        /// Redirect URL for this payment method
        /// </summary>
        [JsonProperty("redirect")]
        public string? Redirect { get; set; }

        /// <summary>
        /// Logo URL for this payment method
        /// </summary>
        [JsonProperty("logo")]
        public string Logo { get; set; }
    }
}

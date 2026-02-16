using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tradify.Data.Helpers.Fawaterak.Einvoice
{
    public class RedirectionUrlsModel
    {
        /// <summary>
        /// URL to redirect to on successful payment
        /// </summary>
        [JsonProperty("successUrl")]
        [Url]
        public string? OnSuccess { get; set; }

        /// <summary>
        /// URL to redirect to on failed payment
        /// </summary>
        [JsonProperty("failUrl")]
        [Url]
        public string? OnFailure { get; set; }

        /// <summary>
        /// URL to redirect to on pending payment
        /// </summary>
        [JsonProperty("pendingUrl")]
        [Url]
        public string? OnPending { get; set; }
    

    }
}

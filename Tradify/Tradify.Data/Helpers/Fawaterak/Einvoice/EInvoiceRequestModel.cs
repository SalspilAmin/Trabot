using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tradify.Data.Helpers.Fawaterak.Einvoice
{
    public class EInvoiceRequestModel
    {
        [JsonProperty("payment_method_id")]
        public int? PaymentMethodId { get; set; }

        [JsonProperty("customer")]
        [Required]
        public required CustomerModel Customer { get; set; }

        [JsonProperty("cartItems")]
        [MinLength(1)]
        [Required]
        public List<CartItemModel> CartItems { get; set; }

        [JsonProperty("cartTotal")]

        public decimal CartTotal =>
            CartItems.Sum(item => item.Price * item.Quantity);

        [JsonProperty("currency")]
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public const string Currency  = "EGP";


        [JsonProperty("payLoad")]
        public InvoicePayload? PayLoad { get; set; }

        /// <summary>
        /// URLs for payment result redirections
        /// </summary>
        [JsonProperty("redirectionUrls")]
        public RedirectionUrlsModel? RedirectionUrls { get; set; }

    }
}

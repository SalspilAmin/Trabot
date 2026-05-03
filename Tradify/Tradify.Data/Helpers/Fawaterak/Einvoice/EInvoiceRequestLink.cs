using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tradify.Data.Helpers.Fawaterak.Einvoice
{
    public class EInvoiceRequestLink
    {

        public EInvoiceRequestLink(CustomerModelJsonlink customer) {
      this.Customer= customer;
      
        }
        [JsonProperty("cartTotal")]

        public string CartTotal =>
            CartItems.Sum(item => item.Price * item.Quantity).ToString();
        [JsonProperty("currency")]
        [Required]
        [StringLength(3, MinimumLength = 3)]
        public  string Currency = "EGP";

        [JsonProperty("customer")]
        [Required]
        public required CustomerModelJsonlink Customer { get; set; }

        [JsonProperty("cartItems")]
        [MinLength(1)]
        [Required]
        public List<CartItemModel> CartItems { get; set; }



        public int orderId { get; set; }


        [JsonProperty("payLoad")]
        public InvoicePayload? PayLoad { get; set; }

        /// <summary>
        /// URLs for payment result redirections
        /// </summary>
        [JsonProperty("redirectionUrls")]
        public RedirectionUrlsModel? RedirectionUrls { get; set; }
    }
    public class CustomerModelJsonlink

    {

        public CustomerModelJsonlink() { }
        [JsonProperty("first_name")]
        [Required]
        public  string FirstName { get; set; }

        /// <summary>
        /// Customer's last name
        /// </summary>
        [JsonProperty("last_name")]
        [Required]
        public required string LastName { get; set; }

        /// <summary>
        /// Customer's email address
        /// </summary>
        [JsonProperty("email")]
        [EmailAddress]
        public string? Email { get; set; }

        /// <summary>
        /// Customer's phone number
        /// </summary>
        [JsonProperty("phone")]
        [Phone]
        public string? Phone { get; set; }
    }
}

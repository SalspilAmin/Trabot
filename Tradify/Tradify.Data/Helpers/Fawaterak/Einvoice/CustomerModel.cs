using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tradify.Data.Helpers.Fawaterak.Einvoice
{
    public class CustomerModel
    {

        /// <summary>
        /// Unique customer identifier in your system
        /// </summary>
        [JsonProperty("customer_unique_id")]
        public string? CustomerId { get; set; }

        /// <summary>
        /// Customer's first name
        /// </summary>
        [JsonProperty("first_name")]
        [Required]
        public required string FirstName { get; set; }

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

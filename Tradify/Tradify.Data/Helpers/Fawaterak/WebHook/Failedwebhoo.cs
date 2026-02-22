using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tradify.Data.Helpers.Fawaterak.WebHook
{
    public class Failedwebhoo
    {
        /// <summary>
        /// Invoice ID from failed transaction
        /// </summary>
        [FromForm(Name = "invoice_id")]
        [Required]
        public long InvoiceId { get; set; }

        /// <summary>
        /// Invoice key from failed transaction
        /// </summary>
        [FromForm(Name = "invoice_key")]
        [Required]
        public string InvoiceKey { get; set; }

        /// <summary>
        /// Error message describing the failure
        /// </summary>
        [FromForm(Name = "errorMessage")]
        [Required]
        public string ErrorMessage { get; set; }
    }
}

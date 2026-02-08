using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Data.Helpers.Fawaterak
{
    public class CardPaymentResponse
    {
        [JsonProperty("data")] public CardPaymentResponseDataModel Data { get; set; }

        public class CardPaymentResponseDataModel : BasePaymentDataResponse
        {
            [JsonProperty("payment_data")] public CardPaymentData PaymentData { get; set; }

            public class CardPaymentData
            {
                [JsonProperty("redirectTo")] public string RedirectTo { get; set; }
            }
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using Tradify.Data.Enums;
using Tradify.Data.Enums.Fawaterak;
using Tradify.Data.Helpers.Fawaterak;
using Tradify.Data.Helpers.Fawaterak.Einvoice;
using Tradify.Data.Helpers.Fawaterak.WebHook;
using Tradify.Service.AbstractsServices.FawaterakServices;
using Tradify.Service.AbstractsServices.IdentityServices;

namespace Tradify.Service.Services.FawaterakServices
{
    public class FawaterakServices : IFawaterakServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly FawaterakOptions fawaterakOptions;
        private readonly IUserService _userService;

        public FawaterakServices(IHttpClientFactory httpClientFactory, FawaterakOptions fawaterakOptions,IUserService userService)
        {
            _httpClientFactory = httpClientFactory;
            this.fawaterakOptions = fawaterakOptions;
            this._userService = userService;
        }

        public async Task<EInvoiceResponseDataModel?> CreateEInvoiceAsync(EInvoiceRequestModel eInvoice)
        {
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post,$"{fawaterakOptions.BaseUrl}/createInvoiceLink");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", fawaterakOptions.ApiKey);
            request.Content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            request.Content= new StringContent(JsonConvert.SerializeObject(eInvoice));

            var response= client.SendAsync(request).Result;
            if (response.IsSuccessStatusCode)
            {
                var resposnecontent = await response.Content.ReadAsStringAsync();
                var _response= JsonConvert.DeserializeObject<EInvoiceResponseModel>(resposnecontent);
                return _response!.Data;
            }
            return null; 
        }

        public async Task<IList<Tradify.Data.Helpers.Fawaterak.PaymentMethod>> GetPaymentMethodsAsync() { 
            var client = _httpClientFactory.CreateClient();
           
            var request = new HttpRequestMessage(HttpMethod.Get, $"{fawaterakOptions.BaseUrl}/getPaymentmethods");
            request.Headers.Add("Authorization", $"Bearer {fawaterakOptions.ApiKey}");


            request.Content = new StringContent(string.Empty, Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var resposnecontent = await response.Content.ReadAsStringAsync();
                var _response = JsonConvert.DeserializeObject<PaymentMethodsResponse>(resposnecontent);
                foreach (var item in _response.Data)
                {
                    item.Id = (int)await GetPaymentItemFromEnum(item.PaymentId, _response.Data);
                }
                return _response.Data  ;
            }
            return null;

        }
       public async Task<FawaterakPaymentFMethods> GetPaymentItemFromEnum(int paymentMethodId, IList<Tradify.Data.Helpers.Fawaterak.PaymentMethod>? paymentMethods=null)
        {
            var methods = paymentMethods ?? await GetPaymentMethodsAsync();
            var method = methods?.FirstOrDefault(x => x.PaymentId == paymentMethodId);
            var name = method.NameEn;

            if (name.Contains("Fawry", StringComparison.OrdinalIgnoreCase))
                return FawaterakPaymentFMethods.Fawry;

            if (name.Contains("Meeza", StringComparison.OrdinalIgnoreCase) ||
                name.Contains("Wallet", StringComparison.OrdinalIgnoreCase))
                return FawaterakPaymentFMethods.EWallet;

            return FawaterakPaymentFMethods.Card;

        }

        public async Task<(BasePaymentDataResponse?,string)> GeneralPay(EInvoiceRequestModel invoice)
        {
            var client = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"{fawaterakOptions.BaseUrl}/invoiceInitPay");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", fawaterakOptions.ApiKey);
            request.Content = new StringContent(JsonConvert.SerializeObject(invoice), Encoding.UTF8, "application/json");
            var isemail =  _userService.IsEmail(invoice.Customer.Email);
            var isphone = _userService.IsPhone(invoice.Customer.Phone);
            if (!isemail && !isphone)
            {
                return (null, "EmailandPhoneAreNotFound");
            }
              var response = await client.SendAsync(request);
               var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
               
               
                var method = await GetPaymentItemFromEnum(invoice.PaymentMethodId.Value);
                switch (method)
                {
                    case FawaterakPaymentFMethods.Fawry:
                        var _fawryResponse = JsonConvert.DeserializeObject<FawryPaymentResponse>(responseContent);
                        return (_fawryResponse!.Data,"Success");
                   
                     
                    case FawaterakPaymentFMethods.Card:
                        var _cardResponse = JsonConvert.DeserializeObject<CardPaymentResponse>(responseContent);
                        return (_cardResponse!.Data, "Success");
                   
                }



            }


            return (null, responseContent);



        }

        public bool VerifyWebhook(WebHookModel webHook)
        {
            var generatedHashKey =
                GenerateHashKeyForWebhookVerification(webHook.InvoiceId, webHook.InvoiceKey, webHook.PaymentMethod);
            return generatedHashKey == webHook.HashKey;
        }

        public bool VerifyCancelTransaction(CancelTransactionModel cancelTransaction)
        {
            var generatedHashKey = GenerateHashKeyForCancelTransaction(cancelTransaction.ReferenceId, cancelTransaction.PaymentMethod);
            return generatedHashKey == cancelTransaction.HashKey;
        }

        public bool VerifyApiKeyTransaction(string apiKey)
        {
            return apiKey == fawaterakOptions.ApiKey;
        }


        #region Generate HashKey
        private string GenerateHashKeyForWebhookVerification(long invoiceId, string invoiceKey, string paymentMethod)
        {
            var queryParam = $"InvoiceId={invoiceId}&InvoiceKey={invoiceKey}&PaymentMethod={paymentMethod}";
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(fawaterakOptions.ApiKey));
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(queryParam));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }
        private string GenerateHashKeyForCancelTransaction(string referenceId, string paymentMethod)
        {
            var queryParam = $"referenceId={referenceId}&PaymentMethod={paymentMethod}";
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(fawaterakOptions.ApiKey));
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(queryParam));

            return BitConverter.ToString(hashBytes).Replace("-","").ToString();

        }
        #endregion
        }
}

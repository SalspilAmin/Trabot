using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Data.Enums.Fawaterak;
using Tradify.Data.Helpers.Fawaterak;
using Tradify.Data.Helpers.Fawaterak.Einvoice;
using Tradify.Data.Helpers.Fawaterak.WebHook;

namespace Tradify.Service.AbstractsServices.FawaterakServices
{
    public interface IFawaterakServices
    {
     public Task<EInvoiceResponseDataModel?> CreateEInvoiceAsync(EInvoiceRequestLink eInvoice);
        public  Task<IList<Tradify.Data.Helpers.Fawaterak.PaymentMethod>> GetPaymentMethodsAsync();
        public Task<FawaterakPaymentFMethods?> GetPaymentItemFromEnum(int paymentMethodId, IList<Tradify.Data.Helpers.Fawaterak.PaymentMethod>? paymentMethods);
        public Task<(BasePaymentDataResponse?, string)> GeneralPay(EInvoiceRequestModel invoice);
        public bool VerifyApiKeyTransaction(string apiKey);
        public bool VerifyCancelTransaction(CancelTransactionModel cancelTransaction);
        public bool VerifyWebhook(WebHookModel webHook);
        public Task<string> WebHookFawaterakEnvoiceLink(Tradify.Data.Entities.Orders order,string Payment_status);


    }
}

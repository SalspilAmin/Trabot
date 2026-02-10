using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Enums.Fawaterak;
using Tradify.Data.Helpers.Fawaterak;
using Tradify.Data.Helpers.Fawaterak.Einvoice;

namespace Tradify.Service.AbstractsServices.FawaterakServices
{
    public interface IFawaterakServices
    {
     public Task<EInvoiceResponseDataModel?> CreateEInvoiceAsync(EInvoiceRequestModel eInvoice);
        public  Task<IList<Tradify.Data.Helpers.Fawaterak.PaymentMethod>> GetPaymentMethodsAsync();
        public Task<FawaterakPaymentFMethods> GetPaymentItemFromEnum(int paymentMethodId, IList<Tradify.Data.Helpers.Fawaterak.PaymentMethod>? paymentMethods);
        public Task<(BasePaymentDataResponse?, string)> GeneralPay(EInvoiceRequestModel invoice);


    }
}

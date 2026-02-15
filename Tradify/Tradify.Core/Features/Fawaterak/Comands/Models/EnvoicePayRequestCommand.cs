using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Helpers.Fawaterak;
using Tradify.Data.Helpers.Fawaterak.Einvoice;

namespace Tradify.Core.Features.Fawaterak.Comands.Models
{
    public class EnvoicePayRequestCommand :EInvoiceRequestModel,IRequest<Response<BasePaymentDataResponse>>
    {
        public int OrderId
            { get; set; }
    }
}

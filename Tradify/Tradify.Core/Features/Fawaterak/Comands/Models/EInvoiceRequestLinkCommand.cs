using MediatR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Helpers.Fawaterak.Einvoice;

namespace Tradify.Core.Features.Fawaterak.Comands.Models
{
    public class EInvoiceRequestLinkCommand :EInvoiceRequestModel ,IRequest<Response<EInvoiceResponseDataModel>>
    {
       
    }
}

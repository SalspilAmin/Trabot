using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Fawaterak.Comands.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Helpers.Fawaterak.Einvoice;
using Tradify.Service.AbstractsServices.FawaterakServices;

namespace Tradify.Core.Features.Fawaterak.Comands.Handler
{
    public class FawaterakCommandHandler : ResponseHandler,IRequestHandler<EInvoiceRequestLinkCommand,Response<EInvoiceResponseDataModel>>
    {
        private readonly LocalizationService localization;
        private readonly IFawaterakServices fawaterakServices;

        public FawaterakCommandHandler(LocalizationService localization, IFawaterakServices fawaterak) : base(localization)
        {
            this.localization = localization;
            this.fawaterakServices = fawaterak;
        }

        public async Task<Response<EInvoiceResponseDataModel>> Handle(EInvoiceRequestLinkCommand request, CancellationToken cancellationToken)
        {
            var result = await fawaterakServices.CreateEInvoiceAsync(request);

            if (result != null)
            {
                return Success(result);
            }
            return BadRequest<EInvoiceResponseDataModel>(localization.Get("TryToRegisterAgain"));
        }

       
    }
}

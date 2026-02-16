using MediatR;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Fawaterak.Queries.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Helpers.Fawaterak;
using Tradify.Service.AbstractsServices.FawaterakServices;

namespace Tradify.Core.Features.Fawaterak.Queries.Handler
{
    public class FawaterakQueriesHandler : ResponseHandler, IRequestHandler<GetPaymentMehtodsQuery, Response<IList<Tradify.Data.Helpers.Fawaterak.PaymentMethod>>>
    {
        private readonly LocalizationService localization;
        private readonly IFawaterakServices fawaterakServices;

     

        public FawaterakQueriesHandler(LocalizationService localization,IFawaterakServices fawaterakServices) : base(localization)
        {
            this.localization = localization;
            this.fawaterakServices = fawaterakServices;
        }
        public async Task<Response<IList<Tradify.Data.Helpers.Fawaterak.PaymentMethod>>> Handle(GetPaymentMehtodsQuery request, CancellationToken cancellationToken)
        {
            var result = await fawaterakServices.GetPaymentMethodsAsync();

            if(result != null)  return Success(result);

            return BadRequest<IList<Tradify.Data.Helpers.Fawaterak.PaymentMethod>>(localization.Get("TryToRegisterAgain"));



        }
    }
}

using MediatR;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Core.Features.Fawaterak.Queries.Models;
using Tradify.Core.Resources.Service;
using Tradify.Data.Helpers.Fawaterak;
using Tradify.Infrastructure.Context;
using Tradify.Service.AbstractsServices;
using Tradify.Service.AbstractsServices.FawaterakServices;

namespace Tradify.Core.Features.Fawaterak.Queries.Handler
{
    public class FawaterakQueriesHandler : ResponseHandler, IRequestHandler<GetPaymentMehtodsQuery, Response<IList<Tradify.Data.Helpers.Fawaterak.PaymentMethod>>>
        ,IRequestHandler<WebHookFawaterakEnvoiceLinkQuery,Response<string>>
   
    {
        
        private readonly LocalizationService localization;
        private readonly IFawaterakServices fawaterakServices;
        private readonly IOrdersService ordersService;
        private readonly ApplicationDbContext context;

     

        public FawaterakQueriesHandler(LocalizationService localization,IFawaterakServices fawaterakServices
            ,IOrdersService ordersService, ApplicationDbContext context) : base(localization)
        {
            this.localization = localization;
            this.fawaterakServices = fawaterakServices;
            this.ordersService = ordersService;
            this.context = context; 
        }
        public async Task<Response<IList<Tradify.Data.Helpers.Fawaterak.PaymentMethod>>> Handle(GetPaymentMehtodsQuery request, CancellationToken cancellationToken)
        {
            var result = await fawaterakServices.GetPaymentMethodsAsync();

            if(result != null)  return Success(result);

            return BadRequest<IList<Tradify.Data.Helpers.Fawaterak.PaymentMethod>>(localization.Get("TryToRegisterAgain"));



        }

        public async Task<Response<string>> Handle(WebHookFawaterakEnvoiceLinkQuery request, CancellationToken cancellationToken)
        {
            var order = context.Orders.FirstOrDefault(x=>x.invoice_id==request.InvoiceId);
            var result= await fawaterakServices.WebHookFawaterakEnvoiceLink(order,request.InvoiceStatus);


            if(result == "Success") return Success(localization.Get("Success"));

            return BadRequest<string>(localization.Get("NotFound"));

        }

      

    
    }
}

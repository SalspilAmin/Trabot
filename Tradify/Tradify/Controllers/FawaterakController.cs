using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Tradify.Bases;
using Tradify.Core.Features.Fawaterak.Comands.Models;
using Tradify.Core.Features.Fawaterak.Queries.Models;
using Tradify.Data.AppMetaData;

namespace Tradify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FawaterakController : AppControllerBase
    {
        [HttpGet(Router.Fawaterak.GetPaymentMehtods)]

        public async Task<IActionResult> GetPaymentMethods()
        {
            var result= await Mediator.Send(new GetPaymentMehtodsQuery());
            return NewResult(result);

        }
        [HttpPost(Router.Fawaterak.EInvoiceLink)]
        public async Task<IActionResult> EInvoiceLink(EInvoiceRequestLinkCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);

        }

        [HttpPost(Router.Fawaterak.invoiceInitPay)]

        public async Task<IActionResult> InvoiceInitPay([FromBody] EnvoicePayRequestCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }
        [HttpPost(Router.Fawaterak.Webhookpaid_json)]
        public async Task<IActionResult> WebhookPaid([FromBody] WebhookPaidCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }
        [HttpPost(Router.Fawaterak.WebhookCancel)]

        public async Task<IActionResult> WebhookCancel([FromBody] WebhookCancelCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }
        [HttpPost(Router.Fawaterak.Webhookfailed)]
        public async Task<IActionResult> WebhookFailed([FromBody] WebhookFailedCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);
        }

    }
}

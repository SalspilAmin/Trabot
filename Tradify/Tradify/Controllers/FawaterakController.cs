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

        public async Task<IActionResult> GetPaymentMethods(GetPaymentMehtodsQuery request)
        {
            var result= await Mediator.Send(request);
            return NewResult(result);

        }
        [HttpPost(Router.Fawaterak.EInvoiceLink)]
        public async Task<IActionResult> EInvoiceLink(EInvoiceRequestLinkCommand request)
        {
            var result = await Mediator.Send(request);
            return NewResult(result);

        }


    }
}

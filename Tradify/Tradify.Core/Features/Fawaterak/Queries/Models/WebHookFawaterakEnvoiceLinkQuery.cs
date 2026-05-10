using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Helpers.Fawaterak.WebHook;

namespace Tradify.Core.Features.Fawaterak.Queries.Models
{
    public class WebHookFawaterakEnvoiceLinkQuery : WebHookModel,IRequest<Response<string>>
    {
    }
}

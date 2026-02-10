
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Fawaterak.Queries.Models
{
    public class GetPaymentMehtodsQuery  : IRequest<Response<IList<Tradify.Data.Helpers.Fawaterak.PaymentMethod>>>
    {

    }
}

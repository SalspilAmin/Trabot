using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Fawaterak.Queries.Models
{
    public class FailedTransactionQuery : IRequest<Response<string>>
    {
        public FailedTransactionQuery() { }
    }
}

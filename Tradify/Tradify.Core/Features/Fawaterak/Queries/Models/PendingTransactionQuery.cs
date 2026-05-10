using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Fawaterak.Queries.Models
{
    public  class PendingTransactionQuery : IRequest<Response<string>>
    {
        public string invoice_id { get; set; }
        public PendingTransactionQuery( string invoice_id) {
        this.invoice_id = invoice_id;
        }
    }
}

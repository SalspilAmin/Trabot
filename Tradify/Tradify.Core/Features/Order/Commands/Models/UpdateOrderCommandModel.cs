using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.Order.Commands.Models
{
    public class UpdateOrderCommandModel : IRequest<Response<int>>
    {
        public int Id { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
      
        public long? invoice_id { get; set; }
        public string? invoice_key { get; set; }

    }
}

using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Order.Commands.Models
{
    public class DeleteOrderCommand : IRequest<Response<string>>
    {
        public int OrderId { get; set; }
        public DeleteOrderCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}

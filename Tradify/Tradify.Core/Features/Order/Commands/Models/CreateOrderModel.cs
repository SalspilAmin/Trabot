
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Entities;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.Order.Commands.Models
{
    public class CreateOrderModel :   IRequest<Response<int>>
    {
        public int CustomerId { get; set; }
        public string ShippingAddress { get; set; }


        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;

        public int CartId { get; set; }

        public decimal? ShippingPrice { get; set; }
       

        
    }
}

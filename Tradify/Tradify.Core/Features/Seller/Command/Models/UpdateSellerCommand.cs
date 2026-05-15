using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Seller.Command.Models
{
    public class UpdateSellerCommand : IRequest<Response<string>>
    {
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
    }
}

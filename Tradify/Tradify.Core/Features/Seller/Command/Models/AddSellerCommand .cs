using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Seller.Command.Models
{
    public class AddSellerCommand : IRequest<Response<string>>
    {
        public string EmailOrPhone { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string BusinessName { get; set; }
        public string BusinessType { get; set; }
    }
}

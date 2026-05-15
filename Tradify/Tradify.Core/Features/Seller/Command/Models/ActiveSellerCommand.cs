using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Seller.Command.Models
{
    public class ActiveSellerCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public ActiveSellerCommand(int id)
        {
            Id = id;
        }
    }
}

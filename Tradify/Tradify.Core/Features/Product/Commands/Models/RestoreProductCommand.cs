using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Product.Commands.Models
{
    public class RestoreProductCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }


        public RestoreProductCommand(int id)
        {
            Id = id;
        }
    }
}

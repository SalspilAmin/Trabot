using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.ProductVariant.Commands.Models
{
    public class RestoreProductVariantCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public RestoreProductVariantCommand(int id)
        {
            Id = id;
        }
    

    }
}

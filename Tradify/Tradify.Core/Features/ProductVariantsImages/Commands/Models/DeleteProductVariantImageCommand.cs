using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.ProductVariantsImages.Commands.Models
{
     public class DeleteProductVariantImageCommand : IRequest<Response<string>>
    {
        public int ImageId { get; set; }
        public DeleteProductVariantImageCommand(int ImageId)
        {
           this.ImageId = ImageId;
        }
    }
}

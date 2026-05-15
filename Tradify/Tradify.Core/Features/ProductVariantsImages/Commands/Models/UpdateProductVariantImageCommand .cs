using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.ProductVariantsImages.Commands.Models
{
    public class UpdateProductVariantImageCommand : IRequest<Response<string>>
    {
        public int ImageId { get; set; }
        public IFormFile Image { get; set; }
    }
    
}

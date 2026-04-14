using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.ProductVariantsImages.Commands.Models
{
    public class AddProductVariantImageCommand : IRequest<Response<string>>
    {
        public int ProductVariantId { get; set; }
        public IFormFile Image { get; set; }

        public bool IsMain { get; set; } = false;
        public int SortOrder { get; set; } = 0;
    }
}

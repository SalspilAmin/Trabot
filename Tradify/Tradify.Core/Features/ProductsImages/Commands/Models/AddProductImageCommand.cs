using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.ProductsImages.Commands.Models
{
    public class AddProductImageCommand : IRequest<Response<string>>
    {
        public int ProductId { get; set; }
        public IFormFile Image { get; set; }

        public bool IsMain { get; set; } = false;
        public int SortOrder { get; set; } = 0;
    }
}

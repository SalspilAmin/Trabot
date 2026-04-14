using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Store.Commands.Models
{
    public class AddStoreWithImageCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int SellerId { get; set; }

        public IFormFile Image { get; set; } 
    }
}

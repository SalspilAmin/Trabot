using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.StoreImage.Commands.Models
{
    public class AddStoreImageCommand : IRequest<Response<string>>
    {
        
        public int StoreId { get; set; }
        public IFormFile Image { get; set; }

       
    }
}

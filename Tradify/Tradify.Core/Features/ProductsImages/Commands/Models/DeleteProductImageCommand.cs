using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.ProductsImages.Commands.Models
{
     public class DeleteProductImageCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteProductImageCommand(int id)
        {
           this.Id = id;
        }
    }
}

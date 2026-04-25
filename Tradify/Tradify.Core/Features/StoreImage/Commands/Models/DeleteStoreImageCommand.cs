using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.StoreImage.Commands.Models
{
    public class DeleteStoreImageCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public DeleteStoreImageCommand(int id)
        {
            this.Id = id;
        }
    }
}

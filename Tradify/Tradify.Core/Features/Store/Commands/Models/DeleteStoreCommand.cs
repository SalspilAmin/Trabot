using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Store.Commands.Models
{
   public  class DeleteStoreCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public DeleteStoreCommand(int id)
        {
            Id = id;
        }
    }
}

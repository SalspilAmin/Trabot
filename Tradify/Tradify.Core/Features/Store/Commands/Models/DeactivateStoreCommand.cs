using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Store.Commands.Models
{
    public class DeactivateStoreCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public DeactivateStoreCommand(int id)
        {
            Id = id;
        }
    }
}

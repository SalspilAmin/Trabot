using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Store.Commands.Models
{
    public class ActivateStoreCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public ActivateStoreCommand(int id)
        {
            Id = id;
        }

    }
}

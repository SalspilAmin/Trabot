using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Store.Commands.Models
{
    public class RestoreStoreCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public RestoreStoreCommand(int id)
        {
            Id = id;
        }
    }
}

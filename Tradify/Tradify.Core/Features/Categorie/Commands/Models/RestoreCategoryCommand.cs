using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Categorie.Commands.Models
{
    public class RestoreCategoryCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }

        public RestoreCategoryCommand(int id)
        {
            Id = id;
        }
    
    }
}

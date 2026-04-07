using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Categorie.Commands.Models
{
   
        public class DeleteCategoryCommand : IRequest<Response<string>>
        {
            public int Id { get; set; }
            public DeleteCategoryCommand(int id)
            {
                Id = id;
            }

        }
    
}

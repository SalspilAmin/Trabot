using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Categorie.Commands.Models
{
    public class AddCategoryCommand : IRequest<Response<string>>
    {
        public string Name { get; set; } 

        public int? ParentCategoryId { get; set; }
    }
}

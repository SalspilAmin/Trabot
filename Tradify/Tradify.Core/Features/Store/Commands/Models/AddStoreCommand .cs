using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tradify.Core.Bases;
using Tradify.Data.Entities;

namespace Tradify.Core.Features.Store.Commands.Models
{
    public class AddStoreCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int SellerId { get; set; }
     
 
    }
}

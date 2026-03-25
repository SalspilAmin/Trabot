using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Bases;

namespace Tradify.Core.Features.Store.Commands.Models
{
    public class UpdateStoreCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string Name { get; set; }     
        public string Description { get; set; }  
            
        public int SellerId { get; set; }       
    }
}

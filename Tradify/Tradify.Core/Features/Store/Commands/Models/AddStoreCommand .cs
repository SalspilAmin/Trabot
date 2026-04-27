using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using Tradify.Core.Bases;
using Tradify.Data.Entities;
using Tradify.Data.Enums;

namespace Tradify.Core.Features.Store.Commands.Models
{
    public class AddStoreCommand : IRequest<Response<string>>
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int SellerId { get; set; }

        public StoreType Type { get; set; }


    }
}

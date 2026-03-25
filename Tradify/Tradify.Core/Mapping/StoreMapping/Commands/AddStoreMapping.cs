using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.Store.Commands.Models;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.StoreMapping
{
    public partial class StoreProfile 
    {
        public void AddStoreMapping()
        {
            CreateMap<AddStoreCommand, Stores>();

        }
    }
}

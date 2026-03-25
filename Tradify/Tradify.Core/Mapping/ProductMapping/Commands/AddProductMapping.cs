using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.ProductMapping
{
    public partial class ProductVariantProfile
    {
        public void AddProductMapping() 
        {
            CreateMap<AddProductCommand, Products>();
          

        }
    }
}

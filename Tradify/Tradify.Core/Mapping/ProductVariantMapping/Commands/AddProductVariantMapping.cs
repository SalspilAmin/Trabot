using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Tradify.Core.Bases;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.ProductVariant.Commands.Models;

using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.ProductVariantMapping
{
    public partial class ProductVariantProfile 
    {
        public void AddProductVariantMapping()
        {
            CreateMap<AddProductVariantCommand, ProductVariants>();
            CreateMap<AddProductVarintWithImageCommand, ProductVariants>();


        }
    }
}



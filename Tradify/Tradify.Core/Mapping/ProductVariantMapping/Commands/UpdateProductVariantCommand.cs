using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using Tradify.Core.Features.ProductVariant.Commands.Models;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.ProductVariantMapping
{
    public partial class ProductVariantProfile
    {
        public void UpdateProductVariantCommand()
        {
            CreateMap<UpdateProductVariantCommand, ProductVariants>();


        }
    }
}

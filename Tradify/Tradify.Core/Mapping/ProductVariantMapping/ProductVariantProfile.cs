using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.ProductVariantMapping
{
    public partial class  ProductVariantProfile : Profile
    {
        public ProductVariantProfile() 
        {
            AddProductVariantMapping();
            UpdateProductVariantCommand();
            GetProductVariantPaginationMapping();
            GetProductVariantByIdMapping();
        }  
    }
}

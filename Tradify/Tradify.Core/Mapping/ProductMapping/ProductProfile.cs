using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.ProductMapping
{
    public partial class ProductVariantProfile : Profile
    {
        public ProductVariantProfile() 
        {
            AddProductMapping();
            UpdateProductMapping();
            GetProductPaginationMapping();
            GetProductByIdMapping();
            GetSellerProductMapping();
        }
    }
}

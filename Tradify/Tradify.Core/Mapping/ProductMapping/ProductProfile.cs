using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.ProductMapping
{
    public partial class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            AddProductMapping();
        }
    }
}

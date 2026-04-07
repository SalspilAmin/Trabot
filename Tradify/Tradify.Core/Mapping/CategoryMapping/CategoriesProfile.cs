using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tradify.Core.Mapping.CategoryMapping
{
    public partial class CategoriesProfile : Profile
    {
        public CategoriesProfile()
        {
            GetAllCategoriesMapping();
            GetCategoryByIdMapping();
            AddCategoryMapping();
            
        }
    }
    
}

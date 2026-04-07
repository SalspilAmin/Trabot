using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Categorie.Queries.Results;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.CategoryMapping
{
    public partial class CategoriesProfile
    {
        public void GetCategoryByIdMapping() 
        {
            CreateMap<Categories, GetCategoryByIdResponse>()
                .ForMember(dest => dest.Parent, opt => opt.Ignore())
                .ForMember(dest => dest.Children, opt => opt.Ignore());
         
    
            CreateMap<Categories, ParentCategoryDto>();
            CreateMap<Categories, ChildCategoryDto>();
        }
    
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Categorie.Commands.Models;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.CategoryMapping
{
    public partial class CategoriesProfile 
    {
        public void AddCategoryMapping()
        {
            CreateMap<AddCategoryCommand, Categories>();
        }
    
    }
}

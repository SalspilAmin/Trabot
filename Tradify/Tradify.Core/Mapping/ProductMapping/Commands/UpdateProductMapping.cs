using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.ProductMapping
{
    public partial class ProductVariantProfile  { 
        public void  UpdateProductMapping()
        {
            CreateMap<UpdateProductCommand, Products>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); // عشان ما يمسحش حاجة بـ null
        } 

    }
}

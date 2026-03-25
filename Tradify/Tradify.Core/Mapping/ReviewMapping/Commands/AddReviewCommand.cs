using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Core.Features.Product.Commands.Models;
using Tradify.Core.Features.Review.Commands.Models;
using Tradify.Core.Features.Store.Commands.Models;
using Tradify.Data.Entities;

namespace Tradify.Core.Mapping.ReviewMapping
{
    public partial class ReviewProfile 
    {
        public void AddReviewMapping()
        {
            CreateMap<AddReviewCommand, Reviews>();

        }
    }
}

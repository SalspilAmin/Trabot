using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Data.Entities;
using Tradify.Infrastructure.AbstractsRepositories;

namespace Tradify.Service.AbstractsServices
{
    public interface IReviewService : IReviewRepository
    {
        public  Task<(string, int?)> AddReview(Reviews reviews);

    }
}

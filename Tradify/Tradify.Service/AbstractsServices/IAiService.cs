using System;
using System.Collections.Generic;
using System.Text;
using Tradify.Service.Services.Ai;

namespace Tradify.Service.AbstractsServices
{
    public interface IAiService
    {
        Task<List<int>> GetRecommendationsAsync(
        AiRequest request);

    }
}

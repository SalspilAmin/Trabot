using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using Tradify.Service.AbstractsServices;

namespace Tradify.Service.Services.Ai
{
    public class AiService :IAiService
    {
        private readonly HttpClient _httpClient;

        public AiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<int>> GetRecommendationsAsync(AiRequest request)
        {
            var response =
            await _httpClient.PostAsJsonAsync(
                "/recommend",
                request);

            response.EnsureSuccessStatusCode();

            var result =
                await response.Content
                    .ReadFromJsonAsync<AiResponse>();

            return result?.product_ids ?? [];
        }
    }
}

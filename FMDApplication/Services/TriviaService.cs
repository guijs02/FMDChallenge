using FMDApplication.Response;
using FMDApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FMDApplication.Services
{
    public class TriviaService : ITriviaService
    {
        private readonly HttpClient _client;
        private JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true,
            WriteIndented = true
        };
        public TriviaService(IHttpClientFactory client)
        {
            _client = client.CreateClient(nameof(TriviaService));
        }
        public async Task<TriviaApiResponse> GetTriviaAsync()
        {
            var response = await _client.GetAsync(TriviaConfiguration.Endpoint);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch trivia data");
            }

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TriviaApiResponse>(content, options);
        }
    }
}

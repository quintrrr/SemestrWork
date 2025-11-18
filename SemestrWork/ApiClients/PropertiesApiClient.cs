using Core.DTO;
using System.Net;
using System.Net.Http.Json;

namespace SemestrWork.ApiClients
{
    public class PropertiesApiClient
    {
        private readonly HttpClient _http;

        public PropertiesApiClient(HttpClient httpClient)
        {
            _http = httpClient;
        }

        public async Task<List<TPropertyDTO>> GetGroupPropertiesAsync(long id)
        {
            return await _http.GetFromJsonAsync<List<TPropertyDTO>>($"properties/group/{id}") ?? [];
        }

        public async Task<TPropertyDTO?> GetPropertyAsync(long id)
        {
            var response = await _http.GetAsync($"properties/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TPropertyDTO>();
        }

        public async Task<HttpStatusCode> DeletePropertyAsync(long id)
        {
            var response = await _http.DeleteAsync($"properties/{id}");
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> SavePropertyAsync(SavePropertyDTO savePropertyDTO)
        {
            var response = await _http.PostAsJsonAsync("properties/save", savePropertyDTO);
            return response.StatusCode;
        }
    }
}

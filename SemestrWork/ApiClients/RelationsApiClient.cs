using Core.DTO;
using System.Net;
using System.Net.Http.Json;

namespace SemestrWork.ApiClients
{
    public class RelationsApiClient
    {
        private readonly HttpClient _http;

        public RelationsApiClient(HttpClient httpClient)
        {
            _http = httpClient;
        }

        public async Task<List<TRelationDTO>> GetParentRelationsAsync(long parentId)
        {
            return await _http.GetFromJsonAsync<List<TRelationDTO>>($"relations/parent/{parentId}") ?? [];
        }

        public async Task<List<TRelationDTO>> GetChildRelationsAsync(long childId)
        {
            return await _http.GetFromJsonAsync<List<TRelationDTO>>($"relations/child/{childId}") ?? [];
        }

        public async Task<HttpStatusCode> DeleteRelationAsync(long parentId, long childId)
        {
            var response = await _http.DeleteAsync($"relations/{parentId}/{childId}");
            return response.StatusCode;
        }

        public async Task CreateRelationAsync(TRelationDTO dto)
        {
            await _http.PostAsJsonAsync($"relations", dto);
        }
    }
}

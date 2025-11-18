using Core.DTO;
using System.Net;
using System.Net.Http.Json;

namespace SemestrWork.ApiClients
{
    public class GroupsApiClient
    {
        private readonly HttpClient _http;

        public GroupsApiClient(HttpClient httpClient)
        {
            _http = httpClient;
        }

        public async Task<HttpStatusCode> DeleteGroupAsync(long id)
        {
            var response = await _http.DeleteAsync($"groups/{id}");
            return response.StatusCode;
        }

        public async Task<List<TGroupDTO>> GetChildGroupsAsync(long parentId)
        {
            return await _http.GetFromJsonAsync<List<TGroupDTO>>($"groups/{parentId}/children") ?? [];
        }

        public async Task<HttpStatusCode> SaveGroupAsync(SaveGroupDTO dto)
        {
            var response = await _http.PostAsJsonAsync("groups/save", dto);
            return response.StatusCode;
        }

        public async Task<long> GetNextGroupIdAsync()
        {
            return await _http.GetFromJsonAsync<long>("groups/nextGroupId");
        }

        public async Task<TGroupDTO?> GetGroupAsync(long id)
        {
            var response = await _http.GetAsync($"groups/{id}");

            if (response.StatusCode == HttpStatusCode.NotFound)
                return null;

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<TGroupDTO>();
        }
    }
}

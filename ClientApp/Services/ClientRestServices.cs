using ClientApp.Models;
using Shared.Utils;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace ClientApp.Services
{
    public class ClientRestServices
    {
        private readonly HttpClient _httpClient;

        public ClientRestServices(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<IEnumerable<Client>> GetAllClient(string token)
        {
            using (_httpClient)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var baseAddress = new Uri(Constans.SERVICES_BASE_URL + "client");
                var response = await _httpClient.GetFromJsonAsync<List<Client>>(baseAddress);
                return response;
            }
        }

        public async Task<Client> GetClientById(string token, int clientId)
        {
            using (_httpClient)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var baseAddress = new Uri(Constans.SERVICES_BASE_URL + $"client/{clientId}");
                var response = await _httpClient.GetFromJsonAsync<Client>(baseAddress);
                return response;
            }
        }

        public async Task<Client> AddOrUpdateClient(string token, Client client)
        {
            using (_httpClient)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var baseAddress = new Uri(Constans.SERVICES_BASE_URL + "client");

                if (client.ClientId == default)
                {
                    var bodySerialize = JsonSerializer.Serialize(client);
                    var content = new StringContent(bodySerialize, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync(baseAddress, content);

                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var deserializeProduct = JsonSerializer.Deserialize<Client>(responseBody);

                    return deserializeProduct;
                }

                baseAddress = new Uri(Constans.SERVICES_BASE_URL + "client/update");

                var bodySerializeUpdate = JsonSerializer.Serialize(client);
                var contentUpdate = new StringContent(bodySerializeUpdate, Encoding.UTF8, "application/json");
                var responseUpdate = await _httpClient.PostAsync(baseAddress, contentUpdate);

                responseUpdate.EnsureSuccessStatusCode();

                var responseBodyUpdate = await responseUpdate.Content.ReadAsStringAsync();
                var deserializeProductUpdate = JsonSerializer.Deserialize<Client>(responseBodyUpdate);

                return deserializeProductUpdate;
            }
        }

        public async Task<int> DeleteClient(string token, int clientId)
        {
            using (_httpClient)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var baseAddress = new Uri(Constans.SERVICES_BASE_URL + $"client/{clientId}");

                var response = await _httpClient.DeleteAsync(baseAddress);

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var deserialize = JsonSerializer.Deserialize<int>(responseString);

                return deserialize;
            }
        }
    }
}

using Shared.Utils;
using System.Text.Json;
using System.Text;
using Shared.Models;

namespace ClientApp.Services
{
    public class ShopRestServices
    {
        private readonly HttpClient _httpClient;

        public ShopRestServices(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<bool> RegisterSale(string token,int clientId, int productId, int amount)
        {
            using (_httpClient)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var baseAddress = new Uri(Constans.SERVICES_BASE_URL + "shop");

                var body = new RegisterSaleBody
                {
                    clientId = clientId,
                    productId = productId,
                    amount = amount
                };

                var bodySerialize = JsonSerializer.Serialize(body);
                var content = new StringContent(bodySerialize, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(baseAddress, content);

                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();
                var deserialize = JsonSerializer.Deserialize<bool>(responseBody);

                return deserialize;
            }
        }

        public async Task<IEnumerable<SalesResponse>> GetAllSales(string token)
        {
            using (_httpClient)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var baseAddress = new Uri(Constans.SERVICES_BASE_URL + "shop");

                var response = await _httpClient.GetFromJsonAsync<List<SalesResponse>>(baseAddress);
                return response;
            }
        }
    }
}

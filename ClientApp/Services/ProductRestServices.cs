using ClientApp.Models;
using Shared.Utils;
using System.Text;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace ClientApp.Services
{
    public class ProductRestServices
    {
        private readonly HttpClient _httpClient;

        public ProductRestServices(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<IEnumerable<Product>> GetAllProduct(string token)
        {
            using (_httpClient)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var baseAddress = new Uri(Constans.SERVICES_BASE_URL + "product");
                var response = await _httpClient.GetFromJsonAsync<List<Product>>(baseAddress);
                return response;
            }
        }

        public async Task<Product> GetProductById(string token, int productId)
        {
            using (_httpClient)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var baseAddress = new Uri(Constans.SERVICES_BASE_URL + $"product/{productId}");
                var response = await _httpClient.GetFromJsonAsync<Product>(baseAddress);
                return response;
            }
        }

        public async Task<Product> AddOrUpdateProduct(string token, Product product)
        {
            using (_httpClient)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var baseAddress = new Uri(Constans.SERVICES_BASE_URL + "product");

                if (product.ProductId == default)
                {
                    var bodySerialize = JsonSerializer.Serialize(product);
                    var content = new StringContent(bodySerialize, Encoding.UTF8, "application/json");

                    var response = await _httpClient.PostAsync(baseAddress, content);

                    response.EnsureSuccessStatusCode();

                    var responseBody = await response.Content.ReadAsStringAsync();
                    var deserializeProduct = JsonSerializer.Deserialize<Product>(responseBody);

                    return deserializeProduct;
                }

                baseAddress = new Uri(Constans.SERVICES_BASE_URL + "product/update");

                var bodySerializeUpdate = JsonSerializer.Serialize(product);
                var contentUpdate = new StringContent(bodySerializeUpdate, Encoding.UTF8, "application/json");
                var responseUpdate = await _httpClient.PostAsync(baseAddress, contentUpdate);

                responseUpdate.EnsureSuccessStatusCode();

                var responseBodyUpdate = await responseUpdate.Content.ReadAsStringAsync();
                var deserializeProductUpdate = JsonSerializer.Deserialize<Product>(responseBodyUpdate);

                return deserializeProductUpdate;
            }
        }

        public async Task<int> DeleteProduct(string token, int productId)
        {
            using (_httpClient)
            {
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                var baseAddress = new Uri(Constans.SERVICES_BASE_URL + $"product/{productId}");

                var response = await _httpClient.DeleteAsync(baseAddress);

                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var deserialize = JsonSerializer.Deserialize<int>(responseString);

                return deserialize;
            }
        }
    }
}

using Shared.Models;
using Shared.Utils;
using System;
using System.Text.Json;

//@inject IHttpClientFactory ClientFactory

namespace ClientApp.Services
{
    public class JWTResolverServices
    {
        private readonly IHttpClientFactory _clientFactory;

        public JWTResolverServices(IHttpClientFactory clientFactory) => _clientFactory = clientFactory;

        public async Task<string> GetToken()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, 
                                                 Constans.SERVICES_BASE_URL+"JwtGenerator?user=admin&password=123");
            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var token = await JsonSerializer.DeserializeAsync<JSonTokenResponse>(responseStream);
                return token?.Token ?? string.Empty;
            }

            return string.Empty;
        }
    }
}

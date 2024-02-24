using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClientApp.Models
{
    public class Client
    {
        [JsonPropertyName("clientId")]
        public int ClientId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}

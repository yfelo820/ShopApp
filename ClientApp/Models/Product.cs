using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ClientApp.Models
{
    public class Product
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("description")]
        public string Description { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("stock")]
        public int Stock { get; set; }
    }
}

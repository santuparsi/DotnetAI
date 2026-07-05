using System.Text.Json.Serialization;

namespace OllamaApi.Models
{
    public class OllamaResponse
    {
        [JsonPropertyName("response")]
        public string Response { get; set; } = "";
    }
}

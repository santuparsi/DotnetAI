namespace OllamaApi.Models
{
    public class OllamaRequest
    {
        public string Model { get; set; } = "";

        public string Prompt { get; set; } = "";

        public bool Stream { get; set; }
    }
}

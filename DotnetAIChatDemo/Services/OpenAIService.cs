using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using DotnetAIChatDemo.Models;

namespace DotnetAIChatDemo.Services;

public class OpenAIService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public OpenAIService(
        HttpClient httpClient,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<string> GetAIResponseAsync(string prompt)
    {
        var apiKey = _configuration["OpenAI:ApiKey"];
        var endpoint = _configuration["OpenAI:Endpoint"];
        var model = _configuration["OpenAI:Model"];

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey);

        var request = new ChatRequest
        {
            Model = model,
            Messages = new List<Message>
            {
                new Message
                {
                    Role = "user",
                    Content = prompt
                }
            }
        };

        var jsonRequest = JsonSerializer.Serialize(request);

        var content = new StringContent(
            jsonRequest,
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.PostAsync(endpoint, content);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception($"OpenAI API Error: {error}");
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();

        var aiResponse = JsonSerializer.Deserialize<ChatResponse>(
            jsonResponse,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return aiResponse?.Choices?.FirstOrDefault()?.Message?.Content
               ?? "No response from AI.";
    }
}
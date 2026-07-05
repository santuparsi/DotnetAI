using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using OllamaApi.Models;

namespace OllamaApi.Controllers;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IHttpClientFactory _factory;
    private readonly IConfiguration _configuration;
    private readonly ILogger<ChatController> _logger;
    public ChatController(IHttpClientFactory factory, IConfiguration configuration, ILogger<ChatController> logger)
    {
        _factory = factory;
        _configuration = configuration;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Chat(ChatRequest request)
    {
        try
        {
            var client = _factory.CreateClient();
            var model = _configuration["Ollama:Model"];
            OllamaRequest ollamaRequest;
            //change Stream=true Ollama returns multiple JSON objects rather than a single response.
            if (model == null)
            {

                ollamaRequest = new OllamaRequest
                {
                    Model = "phi3",
                    Prompt = request.Prompt,
                    Stream = false
                };
            }
            else
            {
                ollamaRequest = new OllamaRequest
                {
                    Model = model,
                    Prompt = request.Prompt,
                    Stream = false
                };
            }
            var json = JsonSerializer.Serialize(ollamaRequest);
            _logger.LogInformation(json);

            var response = await client.PostAsync(
                "http://localhost:11434/api/generate",
                new StringContent(json, Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
            _logger.LogInformation(response.Content.ToString());
            var result = await response.Content.ReadAsStringAsync();
            _logger.LogInformation(result);

            var aiResponse =
        JsonSerializer.Deserialize<OllamaResponse>(result);

            return Ok(aiResponse);
        }
        catch (Exception ex)
        {

            _logger.LogError(ex.Message);
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }
}
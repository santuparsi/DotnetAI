using DotnetAIChatDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace DotnetAIChatDemo.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly OpenAIService _openAIService;

    public ChatController(OpenAIService openAIService)
    {
        _openAIService = openAIService;
    }

    [HttpPost]
    public async Task<IActionResult> AskAI([FromBody] UserPromptRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Prompt))
        {
            return BadRequest("Prompt cannot be empty.");
        }

        var result = await _openAIService.GetAIResponseAsync(request.Prompt);

        return Ok(new
        {
            Response = result
        });
    }
}

public class UserPromptRequest
{
    public string Prompt { get; set; }
}
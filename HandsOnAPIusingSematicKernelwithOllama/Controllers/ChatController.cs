using HandsOnAPIusingSematicKernelwithOllama.Models;
using HandsOnAPIusingSematicKernelwithOllama.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SemanticKernel.ChatCompletion;

namespace HandsOnAPIusingSematicKernelwithOllama.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly AIService _service;

        public ChatController(AIService service)
        {
            _service = service;
        }
        [HttpPost]
        public async Task<IActionResult> Ask(AskRequest request)
        {
            var response = await _service.AskAsync(request.Prompt);

            return Ok(response);
        }
        [HttpGet("test")]
        public IActionResult Test([FromServices] IChatCompletionService chat)
        {
            return Ok(chat.GetType().FullName);
        }
    }
}

using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

namespace HandsOnAPIusingSematicKernelwithOllama.Services
{
    public class AIService
    {

        private readonly IChatCompletionService _chatService;

        public AIService(IChatCompletionService chatService)
        {
            _chatService = chatService;
        }

        public async Task<string> AskAsync(string prompt)
        {
            var history = new ChatHistory();
            history.AddUserMessage(prompt);

            var response = await _chatService.GetChatMessageContentAsync(history);

            return response.Content ?? string.Empty;
        }
    }
}

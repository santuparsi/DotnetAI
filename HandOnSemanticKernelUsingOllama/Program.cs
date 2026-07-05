using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
namespace HandOnSemanticKernelUsingOllama
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            //create Kernel
            var builder = Kernel.CreateBuilder();
            //add Ollama chat completion service
            builder.AddOllamaChatCompletion(
                modelId: "phi3",
                endpoint: new Uri("http://localhost:11434")
                );
            //build the kernel
            Kernel kernel = builder.Build(); //now the kernel contains AI service,Plunins,Memory, Functions, Agents etc.
                                             
            //get chat completion service from kernel
             //This is Dependency Injection.

            //Instead of creating objects manually

            //Semantic Kernel gives us the registered AI service.
            var chat = kernel.GetRequiredService<IChatCompletionService>();
            //create chat history
            /*
             LLM dont remember the previous conversation, 
            so we need to keep track of the conversation history and pass it to the LLM for context.
            Semantic Kernel stores previous messages.
             */
            var history = new ChatHistory();
            //chat loop
            while (true)
            {
                Console.Write("You : ");

                string? prompt = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(prompt))
                    break;

                history.AddUserMessage(prompt);

                var response =
                    await chat.GetChatMessageContentAsync(history);

                Console.WriteLine();

                Console.WriteLine(response.Content);

                history.AddAssistantMessage(response.Content!);
            }


        }
    }
}

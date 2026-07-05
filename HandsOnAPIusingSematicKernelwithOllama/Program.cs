
using HandsOnAPIusingSematicKernelwithOllama.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.SemanticKernel;

namespace HandsOnAPIusingSematicKernelwithOllama
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();
            //configure the Semantic Kernel and add it to the DI container
            builder.Services.AddKernel();
            //configure the OpenAI chat completion service and add it to the DI container
            builder.Services.AddOpenAIChatCompletion(
             modelId: builder.Configuration["AI:Model"]!,
             endpoint: new Uri(builder.Configuration["AI:Endpoint"]!),
             apiKey: builder.Configuration["AI:ApiKey"]);
            builder.Services.AddScoped<AIService>();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}

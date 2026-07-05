namespace DotnetAIChatDemo.Models;

public class ChatRequest
{
    public string Model { get; set; }
    public List<Message> Messages { get; set; }
}
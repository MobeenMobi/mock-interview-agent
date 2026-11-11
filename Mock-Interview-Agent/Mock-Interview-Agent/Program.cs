
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.AI;
using Microsoft.Agents.AI;
using OllamaSharp;

class Program
{
    static async Task Main(string[] args)
    {
        // Local Ollama model
        string modelId = "llama3.2:3b";

        // Connect to Ollama
        IChatClient chatClient = new OllamaApiClient(
            new Uri("http://localhost:11434"),
            modelId
        );

        // Create AI Agent
        AIAgent agent = chatClient.AsAIAgent(
            name: "MockInterviewAgent",
            instructions: "Send only 3 interview questions as per the "
                        + "skill put by the developer "
                        + "and its timebound for 1 minute"
        // tools: [AIFunctionFactory.Create(StartTimer)]
        );

        // Get user input
        Console.Write("Enter who you are? ");

        string userPrompt = Console.ReadLine() ?? "";

        Console.WriteLine($"User: {userPrompt}\n");

        // Run Agent
        var response = await agent.RunAsync(userPrompt);

        Console.WriteLine($"Agent: {response.Text}");
    }
}

using IAStructuredDataFromImage.Domain;
using Microsoft.Extensions.AI;

namespace IAStructuredDataFromImage.Application;

public class ReceiptExtractionService
{
    private readonly IChatClient _chatClient;

    public ReceiptExtractionService(IChatClient chatClient)
    {
        _chatClient = chatClient;
    }

    public async Task<Receipt?> ExtractAsync(string imagePath)
    {
        if (!File.Exists(imagePath))
            return null;

        var systemPrompt = PromptBuilder.BuildSystemPrompt();

        var userMessage = new ChatMessage(
            ChatRole.User,
            "Extract structured receipt data.");

        userMessage.Contents.Add(
            new DataContent(
                await File.ReadAllBytesAsync(imagePath),
                "image/png"));

        var response = await _chatClient.GetResponseAsync<Receipt>(
            new[] { systemPrompt, userMessage },
            new ChatOptions
            {
                Temperature = 0
            });

        return response.Result;
    }
}

using Microsoft.Extensions.DependencyInjection;
using OllamaSharp;

namespace IAStructuredDataFromImage.Infrastructure;

public static class OllamaConfiguration
{
    public static IServiceCollection AddOllamaVision(
        this IServiceCollection services,
        string endpoint,
        string model)
    {
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri(endpoint),
            Timeout = TimeSpan.FromMinutes(5)
        };

        services.AddChatClient(
            new OllamaApiClient(httpClient, model));

        return services;
    }
}

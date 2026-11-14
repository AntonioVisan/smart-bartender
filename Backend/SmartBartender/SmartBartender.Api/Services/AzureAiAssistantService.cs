using System.Text.Json;
using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.Agents.AI.Foundry;
using SmartBartender.Api.DTOs;

namespace SmartBartender.Api.Services;

public class AzureAiAssistantService
{
    private readonly FoundryAgent _agent;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public AzureAiAssistantService(IConfiguration configuration)
    {
        var projectEndpoint = configuration["Foundry:ProjectEndpoint"]
            ?? throw new InvalidOperationException(
                "Missing Foundry project endpoint.");

        var agentEndpoint = configuration["Foundry:AgentEndpoint"]
            ?? throw new InvalidOperationException(
                "Missing Foundry agent endpoint.");

        var projectClient = new AIProjectClient(
            new Uri(projectEndpoint),
            new AzureCliCredential());

        _agent = projectClient.AsAIAgent(
            new Uri(agentEndpoint));
    }

    public async Task<AssistantResponseDto> SendMessageAsync(
        string message,
        CancellationToken cancellationToken = default)
    {
        var response = await _agent.RunAsync(
            message,
            cancellationToken: cancellationToken);

        if (string.IsNullOrWhiteSpace(response.Text))
        {
            throw new InvalidOperationException(
                "The assistant returned an empty response.");
        }

        try
        {
            var assistantResponse =
                JsonSerializer.Deserialize<AssistantResponseDto>(
                    response.Text, JsonOptions);

            return assistantResponse
                ?? throw new InvalidOperationException(
                    "Failed to deserialize assistant response.");
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException(
                "The assistant returned an invalid JSON response.",
                ex);
        }
    }
}
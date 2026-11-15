using System.Text.Json;
using Azure.AI.Extensions.OpenAI;
using Azure.AI.Projects;
using Azure.Identity;
using OpenAI;
using OpenAI.Responses;
using SmartBartender.Api.DTOs;

namespace SmartBartender.Api.Services;

public class AzureAiAssistantService
{
#pragma warning disable OPENAI001
    private readonly AIProjectClient _projectClient;
    private readonly ProjectConversationsClient _conversationClient;

    private const string AgentName = "SmartBartenderAssistant";

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    public AzureAiAssistantService(IConfiguration configuration)
    {
        var projectEndpoint = configuration["Foundry:ProjectEndpoint"]
            ?? throw new InvalidOperationException(
                "Missing Foundry project endpoint.");

        _projectClient = new AIProjectClient(
            new Uri(projectEndpoint),
            new AzureCliCredential());

        var agentClient = CreateAgentClient();

        _conversationClient = CreateConversationClient(agentClient);
    }

    public async Task<AssistantResponseDto> SendMessageAsync(
        string message,
        string? conversationId = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(conversationId))
        {
            conversationId = await CreateConversationAsync(cancellationToken);
        }

        var response = await SendToAgentAsync(
            message,
            conversationId,
            cancellationToken: cancellationToken);

        var responseText = response.GetOutputText();

        if (string.IsNullOrWhiteSpace(responseText))
        {
            throw new InvalidOperationException(
                "The assistant returned an empty response.");
        }

        try
        {
            var assistantResponse =
                JsonSerializer.Deserialize<AssistantResponseDto>(
                    responseText, JsonOptions);

            if (assistantResponse == null)
            {
                throw new InvalidOperationException(
                    "Failed to deserialize assistant response.");
            }

            assistantResponse.ConversationId = conversationId;

            return assistantResponse;
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException(
                "The assistant returned an invalid JSON response.",
                ex);
        }
    }

    private ProjectResponsesClient CreateAgentClient()
    {
        return _projectClient.ProjectOpenAIClient
            .GetProjectResponsesClientForAgentEndpoint(AgentName);
    }

    private ProjectConversationsClient CreateConversationClient(
        ProjectResponsesClient agentClient)
    {
        return new ProjectConversationsClient(
            agentClient.Pipeline,
            new OpenAIClientOptions
            {
                Endpoint = agentClient.Endpoint
            });
    }

    private async Task<string> CreateConversationAsync(
        CancellationToken cancellationToken)
    {
        var conversation = await _conversationClient
            .CreateProjectConversationAsync(
            cancellationToken: cancellationToken);

        return conversation.Value.Id;
    }

    private ProjectResponsesClient CreateResponseClient(
        string conversationId)
    {
        return _projectClient.ProjectOpenAIClient
            .GetProjectResponsesClientForAgentEndpoint(
            AgentName,
            defaultConversationId: conversationId);
    }

    private async Task<ResponseResult> SendToAgentAsync(
        string message,
        string conversationId,
        CancellationToken cancellationToken)
    {
        var responseClient = CreateResponseClient(conversationId);

        var response = await responseClient.CreateResponseAsync(
            message,
            cancellationToken: cancellationToken);

        return response;
    }
#pragma warning restore OPENAI001
}
using Azure.AI.Projects;
using Azure.Identity;
using Microsoft.Agents.AI.Foundry;

namespace SmartBartender.Api.Services;

public class AzureAiAssistantService
{
    private readonly FoundryAgent _agent;

    public AzureAiAssistantService(IConfiguration configuration)
    {
        var projectEndpoint = configuration["Foundry:ProjectEndpoint"]
            ?? throw new InvalidOperationException(
                "Missing Foundry project endpoint.");

        var agentEndpoint =
            configuration["Foundry:AgentEndpoint"]
            ?? throw new InvalidOperationException(
                "Missing Foundry agent endpoint.");


        var projectClient = new AIProjectClient(
            new Uri(projectEndpoint),
            new AzureCliCredential());

        _agent = projectClient.AsAIAgent(
            new Uri(agentEndpoint));
    }

    public async Task<string> SendMessageAsync(
        string message,
        CancellationToken cancellationToken = default)
    {
        var response = await _agent.RunAsync(
            message,
            cancellationToken: cancellationToken);

        return response.Text;
    }
}
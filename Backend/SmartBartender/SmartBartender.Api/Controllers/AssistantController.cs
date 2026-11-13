using Microsoft.AspNetCore.Mvc;
using SmartBartender.Api.Services;

namespace SmartBartender.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssistantController : ControllerBase
{
    private readonly AzureAiAssistantService _assistantService;

    public AssistantController(AzureAiAssistantService assistantService)
    {
        _assistantService = assistantService;
    }

    public class AssistantRequest
    {
        public string Message { get; set; } = "";
    }

    [HttpPost("chat")]
    public async Task<IActionResult> SendMessageAsync(
        [FromBody] AssistantRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _assistantService.SendMessageAsync(
            request.Message, cancellationToken);

        return Ok(new { response });
    }
}

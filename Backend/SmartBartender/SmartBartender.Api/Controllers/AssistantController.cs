using Microsoft.AspNetCore.Mvc;
using SmartBartender.Api.Services;

namespace SmartBartender.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssistantController : ControllerBase
{
    private readonly AzureAiAssistantService _assistantService;
    private readonly CocktailValidationService _validationService;

    public AssistantController(
        AzureAiAssistantService assistantService,
        CocktailValidationService validationService)
    {
        _assistantService = assistantService;
        _validationService = validationService;
    }

    public class AssistantRequest
    {
        public string Message { get; set; } = "";
        public string? ConversationId { get; set; }
    }

    [HttpPost("chat")]
    public async Task<IActionResult> SendMessageAsync(
        [FromBody] AssistantRequest request,
        CancellationToken cancellationToken)
    {
        var response = await _assistantService.SendMessageAsync(
            request.Message, 
            request.ConversationId,
            cancellationToken);

        _validationService.Validate(response);

        return Ok(response);
    }
}

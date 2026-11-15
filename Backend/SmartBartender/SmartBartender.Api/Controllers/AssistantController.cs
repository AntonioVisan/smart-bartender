using Microsoft.AspNetCore.Mvc;
using SmartBartender.Api.DTOs;
using SmartBartender.Api.Services;

namespace SmartBartender.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AssistantController : ControllerBase
{
    private readonly AzureAiAssistantService _assistantService;
    private readonly CocktailValidationService _validationService;
    private readonly CocktailCalculationService _calculationService;

    public AssistantController(
        AzureAiAssistantService assistantService,
        CocktailValidationService validationService,
        CocktailCalculationService calculationService)
    {
        _assistantService = assistantService;
        _validationService = validationService;
        _calculationService = calculationService;
    }

    public class AssistantRequest
    {
        public string Message { get; set; } = "";
        public string? ConversationId { get; set; }
        public List<int>? GlassSizesMl { get; set; }
    }

    [HttpPost("chat")]
    public async Task<IActionResult> SendMessageAsync(
        [FromBody] AssistantRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Message))
        {
            return BadRequest("Message cannot be empty.");
        }

        var glassSizes = request.GlassSizesMl is { Count: > 0 }
                ? request.GlassSizesMl : [350, 500];

        AssistantResponseDto response;

        try 
        {
            response = await _assistantService.SendMessageAsync(
                request.Message,
                request.ConversationId,
                cancellationToken);
        }
        catch (OperationCanceledException)
        {
            throw;
        }
        catch (Exception)
        {
            return StatusCode(
                StatusCodes.Status502BadGateway,
                "The AI service is currently unavailable.");
        }
        _validationService.Validate(response);

        try
        {
            foreach (var cocktail in response.Cocktails)
            {
                cocktail.Glasses.Clear();

                List<int> ingredientsPercent = cocktail.Ingredients
                    .Select(i => i.Percent)
                    .ToList();

                Dictionary<int, int[]> cocktailVolumes = _calculationService
                    .CalculateCocktailVolumes(glassSizes,
                    ingredientsPercent);

                foreach (var entry in cocktailVolumes)
                {
                    var glass = new GlassDto
                    {
                        GlassSizeMl = entry.Key
                    };

                    for (int i = 0; i < entry.Value.Length; i++)
                    {
                        glass.Ingredients.Add(new IngredientVolumeDto
                        {
                            Name = cocktail.Ingredients[i].Name,
                            Ml = entry.Value[i]
                        });
                    }

                    cocktail.Glasses.Add(glass);
                }
            }
        }
        catch(ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }

        return Ok(response);
    }
}

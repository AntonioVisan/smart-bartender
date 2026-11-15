namespace SmartBartender.Api.DTOs; 

public class AssistantResponseDto
{
    public string Reply { get; set; } = string.Empty;
    public List<CocktailDto> Cocktails { get; set; } = [];
    public string ConversationId { get; set; } = string.Empty;
}

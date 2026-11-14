namespace SmartBartender.Api.DTOs;

public class CocktailDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<IngredientDto> Ingredients { get; set; } = [];
}

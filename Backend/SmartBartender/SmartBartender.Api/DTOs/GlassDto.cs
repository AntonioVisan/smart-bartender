namespace SmartBartender.Api.DTOs;

public class GlassDto
{
    public int GlassSizeMl { get; set; }
    public List<IngredientVolumeDto> Ingredients { get; set; } = [];
}

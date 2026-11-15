namespace SmartBartender.Api.Services;

public class CocktailCalculationService
{
    public int[] CalculateIngredientVolumes(
        int glassSizeMl,
        List<int> percentages)
    {
        ArgumentNullException.ThrowIfNull(percentages);
        
        if (glassSizeMl <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(glassSizeMl),
                "Glass size must be greater than zero.");
        }

        if(percentages.Count == 0)
        {
            throw new ArgumentException(
                "At least one ingredient percentage is required.",
                nameof(percentages));
        }

        if(percentages.Any(p => p < 0))
        {
            throw new ArgumentException(
                "Ingredient percentages cannot be negative.",
                nameof(percentages));
        }

        if (percentages.Any(p => p > 100))
        {
            throw new ArgumentException(
                "Ingredient percentages cannot exceed 100.",
                nameof(percentages));
        }

        long total = percentages.Sum(p => (long)p);

        if (total != 100)
        {
            throw new ArgumentException(
                "The total percentage must be equal to 100.",
                nameof(percentages));
        }
    
        var volumes = new int[percentages.Count];

        int used = 0;

        for (int i = 0; i < percentages.Count - 1; i++)
        {
            volumes[i] = (int)Math.Round(
                glassSizeMl * (double)percentages[i] / total);

            used += volumes[i];
        }

        volumes[^1] = glassSizeMl - used;

        return volumes;
    }

    private void ValidateGlassSizes(List<int> glassSizesMl)
    {
        ArgumentNullException.ThrowIfNull(glassSizesMl);

        if (glassSizesMl.Count == 0)
        {
            throw new ArgumentException(
                "At least one glass size is required.",
                nameof(glassSizesMl));
        }

        if (glassSizesMl.Count > 3)
        {
            throw new ArgumentException(
                "A maximum of three glass sizes is allowed.",
                nameof(glassSizesMl));
        }

        if (glassSizesMl.Any(size => size <= 0))
        {
            throw new ArgumentOutOfRangeException(
                nameof(glassSizesMl),
                 "All glass sizes must be greater than zero.");
        }

        if (glassSizesMl.Any(size => size > 1000))
        {
            throw new ArgumentOutOfRangeException(
                nameof(glassSizesMl),
                "Glass sizes cannot exceed 1000 ml.");
        }
    }

    public Dictionary<int, int[]> CalculateCocktailVolumes(
        List<int> glassSizesMl,
        List<int> percentages)
    {
        ValidateGlassSizes(glassSizesMl);

        if (glassSizesMl.Distinct().Count() != glassSizesMl.Count)
        {
            throw new ArgumentException(
                "Glass sizes cannot contain duplicates.",
                nameof(glassSizesMl));
        }

        var volumes = new Dictionary<int, int[]>();

        foreach(int glassSize in glassSizesMl)
        {
            int[] ingredientVolumes = CalculateIngredientVolumes(glassSize, percentages);
            volumes.Add(glassSize, ingredientVolumes);
        }

        return volumes;
    }
}

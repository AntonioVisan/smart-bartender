using SmartBartender.Api.Services;

namespace SmartBartender.Tests;

public class CocktailCalculationServiceTests
{
    [Fact]
    public void CalculateIngredientVolumes_ShouldReturnCorrectVolumes_WhenInputIsValid()
    {
        // Arrange
        var calculationService = new CocktailCalculationService();

        var percentages = new List<int> { 40, 15, 10, 30, 5 };

        // Act
        var result = calculationService.CalculateIngredientVolumes(
            350, percentages);

        // Assert
        Assert.Equal(new[] { 140, 52, 35, 105, 18 }, result);
    }

    [Fact]
    public void CalculateIngredientVolumes_ShouldReturnVolumesThatSumToGlassSize()
    {
        // Arrange
        var calculationService = new CocktailCalculationService();

        int glassSizeMl = 101;
        var percentages = new List<int> { 50, 50 };

        // Act
        var result = calculationService.CalculateIngredientVolumes(
            glassSizeMl, percentages);

        // Assert
        Assert.Equal(glassSizeMl, result.Sum());
        Assert.Equal(new[] { 50, 51 }, result);
    }

    [Fact]
    public void CalculateIngredientVolumes_ShouldThrowArgumentNullException_WhenPercentagesIsNull()
    {
        // Arrange
        var calculationService = new CocktailCalculationService();

        // Act & Assert
        Assert.Throws<ArgumentNullException>(
            () => calculationService.CalculateIngredientVolumes(
                350, null!));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void CalculateIngredientVolumes_ShouldThrowArgumentOutOfRangeException_WhenGlassSizeIsNotPositive(
        int glassSizeMl)
    {
        // Arrange
        var calculationService = new CocktailCalculationService();

        var percentages = new List<int> { 100 };

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(
            () => calculationService.CalculateIngredientVolumes(
                glassSizeMl, percentages));
    }

    [Fact]
    public void CalculateIngredientVolumes_ShouldThrowArgumentException_WhenPercentagesIsEmpty()
    {
        // Arrange
        var calculationService = new CocktailCalculationService();

        // Act & Assert
        Assert.Throws<ArgumentException>(
            () => calculationService.CalculateIngredientVolumes(
                350, []));
    }

    [Fact]
    public void CalculateIngredientVolumes_ShouldThrowArgumentException_WhenPercentageIsNegative()
    {
        // Arrange
        var calculationService = new CocktailCalculationService();
        var percentages = new List<int> { 110, -10 };

        // Act & Assert
        Assert.Throws<ArgumentException>(
            () => calculationService.CalculateIngredientVolumes(
                350, percentages));
    }

    [Fact]
    public void CalculateIngredientVolumes_ShouldThrowArgumentException_WhenPercentageIsZero()
    {
        // Arrange
        var calculationService = new CocktailCalculationService();
        var percentages = new List<int> { 50, 0, 50 };

        // Act & Assert
        Assert.Throws<ArgumentException>(
            () => calculationService.CalculateIngredientVolumes(
                350, percentages));
    }

    [Fact]
    public void CalculateIngredientVolumes_ShouldThrowArgumentException_WhenPercentageExceeds100()
    {
        // Arrange
        var calculationService = new CocktailCalculationService();
        var percentages = new List<int> { 110 };

        // Act & Assert
        Assert.Throws<ArgumentException>(
            () => calculationService.CalculateIngredientVolumes(
                350, percentages));
    }

    [Theory]
    [InlineData(40, 50)]
    [InlineData(60, 60)]
    public void CalculateIngredientVolumes_ShouldThrowArgumentException_WhenPercentageSumIsNot100(
        int firstPercentage,
        int secondPercentage)
    {
        // Arrange
        var calculationService = new CocktailCalculationService();
        var percentages = new List<int> { firstPercentage, secondPercentage };

        // Act & Assert
        Assert.Throws<ArgumentException>(
            () => calculationService.CalculateIngredientVolumes(
                350, percentages));
    }

    [Fact]
    public void CalculateCocktailVolumes_ShouldReturnCorrectVolumes_WhenInputIsValid()
    {
        // Arrange
        var calculationService = new CocktailCalculationService();

        var glassSizesMl = new List<int> { 200, 300, 450 };
        var percentages = new List<int> { 40, 30, 20, 10 };

        // Act
        var result = calculationService.CalculateCocktailVolumes(
            glassSizesMl, percentages);

        // Assert
        Assert.Equal(3, result.Count);

        Assert.Equal(new[] { 80, 60, 40, 20 }, result[200]);
        Assert.Equal(new[] { 120, 90, 60, 30 }, result[300]);
        Assert.Equal(new[] { 180, 135, 90, 45 }, result[450]);
    }

    [Fact]
    public void CalculateCocktailVolumes_ShouldThrowArgumentNullException_WhenGlassSizesIsNull()
    {
        // Arrange
        var calculationService = new CocktailCalculationService();
        var percentages = new List<int> { 100 };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(
            () => calculationService.CalculateCocktailVolumes(
                null!, percentages));
    }

    [Fact]
    public void CalculateCocktailVolumes_ShouldThrowArgumentException_WhenGlassSizesIsEmpty()
    {
        // Arrange
        var calculationService = new CocktailCalculationService();
        var percentages = new List<int> { 100 };

        // Act & Assert
        Assert.Throws<ArgumentException>(
            () => calculationService.CalculateCocktailVolumes(
                [], percentages));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void CalculateCocktailVolumes_ShouldThrowArgumentOutOfRangeException_WhenGlassSizeIsNotPositive(
        int glassSizeMl)
    {
        // Arrange
        var calculationService = new CocktailCalculationService();

        var glassSizesMl = new List<int> { glassSizeMl };
        var percentages = new List<int> { 100 };

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(
            () => calculationService.CalculateCocktailVolumes(
                glassSizesMl, percentages));
    }

    [Theory]
    [InlineData(1001)]
    [InlineData(1500)]
    public void CalculateCocktailVolumes_ShouldThrowArgumentOutOfRangeException_WhenGlassSizeExceeds1000Ml(
        int glassSizeMl)
    {
        // Arrange
        var calculationService = new CocktailCalculationService();

        var glassSizesMl = new List<int> { glassSizeMl };
        var percentages = new List<int> { 100 };

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(
            () => calculationService.CalculateCocktailVolumes(
                glassSizesMl, percentages));
    }

    [Fact]
    public void CalculateCocktailVolumes_ShouldAllowGlassSizeOf1000Ml()
    {
        // Arrange
        var calculationService = new CocktailCalculationService();

        var glassSizesMl = new List<int> { 1000 };
        var percentages = new List<int> { 100 };

        // Act
        var result = calculationService.CalculateCocktailVolumes(
            glassSizesMl, percentages);

        // Assert
        Assert.Equal(new[] { 1000 }, result[1000]);
    }

    [Fact]
    public void CalculateCocktailVolumes_ShouldThrowArgumentException_WhenGlassSizesContainDuplicates()
    {
        // Arrange
        var calculationService = new CocktailCalculationService();

        var glassSizesMl = new List<int> { 350, 350 };
        var percentages = new List<int> { 100 };

        // Act & Assert
        Assert.Throws<ArgumentException>(
            () => calculationService.CalculateCocktailVolumes(
                glassSizesMl, percentages));
    }

    [Fact]
    public void CalculateCocktailVolumes_ShouldThrowArgumentNullException_WhenPercentagesIsNull()
    {
        // Arrange
        var calculationService = new CocktailCalculationService();

        var glassSizesMl = new List<int> { 350 };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(
            () => calculationService.CalculateCocktailVolumes(
                glassSizesMl, null!));
    }

    [Fact]
    public void CalculateCocktailVolumes_ShouldThrowArgumentException_WhenPercentagesAreInvalid()
    {
        // Arrange
        var calculationService = new CocktailCalculationService();

        var glassSizesMl = new List<int> { 350 };
        var percentages = new List<int> { 40, 40 };

        // Act & Assert
        Assert.Throws<ArgumentException>(
            () => calculationService.CalculateCocktailVolumes(
                glassSizesMl, percentages));
    }
}

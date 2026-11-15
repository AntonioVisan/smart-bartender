using SmartBartender.Api.DTOs;
using SmartBartender.Api.Services;

namespace SmartBartender.Tests;

public class CocktailValidationServiceTests
{
    [Fact]
    public void Validate_ShouldNotThrowException_WhenResponseIsValid()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "I recommend a Vodka Sunrise.",
            Cocktails =
            [
                new CocktailDto
                {
                    Name = "Vodka Sunrise",
                    Description = "A refreshing cocktail.",
                    Ingredients =
                    [
                        new IngredientDto { Name = "Vodka", Percent = 40 },
                        new IngredientDto { Name = "Orange Juice", Percent = 50 },
                        new IngredientDto { Name = "Grenadine", Percent = 10 }
                    ]
                }
            ]
        };

        //Act & Assert
        validationService.Validate(response);
    }

    [Fact]
    public void Validate_ShouldThrowArgumentNullException_WhenResponseIsNull()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        //Act & Assert
        Assert.Throws<ArgumentNullException>(
            () => validationService.Validate(null!));
    }

    [Fact]
    public void Validate_ShouldThrowInvalidOperationException_WhenReplyIsNull()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = null!,
            Cocktails = []
        };

        //Act & Assert
        Assert.Throws<InvalidOperationException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldThrowInvalidOperationException_WhenReplyIsEmpty()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = string.Empty,
            Cocktails = []
        };

        //Act & Assert
        Assert.Throws<InvalidOperationException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldThrowInvalidOperationException_WhenReplyContainsOnlyWhitespace()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "   ",
            Cocktails = []
        };

        //Act & Assert
        Assert.Throws<InvalidOperationException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldThrowInvalidOperationException_WhenCocktailsIsNull()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "Here is my recommendation.",
            Cocktails = null!
        };

        //Act & Assert
        Assert.Throws<InvalidOperationException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldThrowArgumentNullException_WhenCocktailIsNull()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "Here is my recommendation.",
            Cocktails = [null!]
        };

        //Act & Assert
        Assert.Throws<ArgumentNullException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldThrowInvalidOperationException_WhenCocktailNameIsNull()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "Here is my recommendation.",
            Cocktails =
            [
                new CocktailDto
                {
                    Name = null!,
                    Description = "A refreshing cocktail.",
                    Ingredients =
                    [
                        new IngredientDto { Name = "Vodka", Percent = 100 }
                    ]
                }
            ]
        };

        //Act & Assert
        Assert.Throws<InvalidOperationException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldThrowInvalidOperationException_WhenCocktailNameIsEmpty()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "Here is my recommendation.",
            Cocktails =
            [
                new CocktailDto
                {
                    Name = string.Empty,
                    Description = "A refreshing cocktail.",
                    Ingredients =
                    [
                        new IngredientDto { Name = "Vodka", Percent = 100 }
                    ]
                }
            ]
        };

        //Act & Assert
        Assert.Throws<InvalidOperationException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldThrowInvalidOperationException_WhenCocktailNameContainsOnlyWhitespace()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "Here is my recommendation.",
            Cocktails =
            [
                new CocktailDto
                {
                    Name = "   ",
                    Description = "A refreshing cocktail.",
                    Ingredients =
                    [
                        new IngredientDto { Name = "Vodka", Percent = 100 }
                    ]
                }
            ]
        };

        //Act & Assert
        Assert.Throws<InvalidOperationException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldThrowInvalidOperationException_WhenCocktailDescriptionIsNull()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "Here is my recommendation.",
            Cocktails =
            [
                new CocktailDto
                {
                    Name = "Cuba Libre",
                    Description = null!,
                    Ingredients =
                    [
                        new IngredientDto { Name = "Vodka", Percent = 100 }
                    ]
                }
            ]
        };

        //Act & Assert
        Assert.Throws<InvalidOperationException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldThrowInvalidOperationException_WhenCocktailDescriptionIsEmpty()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "Here is my recommendation.",
            Cocktails =
            [
                new CocktailDto
                {
                    Name = "Cuba Libre",
                    Description = string.Empty,
                    Ingredients =
                    [
                        new IngredientDto { Name = "Vodka", Percent = 100 }
                    ]
                }
            ]
        };

        //Act & Assert
        Assert.Throws<InvalidOperationException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldThrowInvalidOperationException_WhenCocktailDescriptionContainsOnlyWhitespace()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "Here is my recommendation.",
            Cocktails =
            [
                new CocktailDto
                {
                    Name = "Cuba Libre",
                    Description = "   ",
                    Ingredients =
                    [
                        new IngredientDto { Name = "Vodka", Percent = 100 }
                    ]
                }
            ]
        };

        //Act & Assert
        Assert.Throws<InvalidOperationException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldThrowArgumentNullException_WhenIngredientsIsNull()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "Here is my recommendation.",
            Cocktails =
            [
                new CocktailDto
                {
                    Name = "Cuba Libre",
                    Description = "A refreshing cocktail.",
                    Ingredients = null!
                }
            ]
        };

        //Act & Assert
        Assert.Throws<ArgumentNullException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldThrowInvalidOperationException_WhenIngredientsIsEmpty()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "Here is my recommendation.",
            Cocktails =
            [
                new CocktailDto
                {
                    Name = "Cuba Libre",
                    Description = "A refreshing cocktail.",
                    Ingredients = []
                }
            ]
        };

        //Act & Assert
        Assert.Throws<InvalidOperationException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldThrowArgumentNullException_WhenIngredientIsNull()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "Here is my recommendation.",
            Cocktails =
            [
                new CocktailDto
                {
                    Name = "Cuba Libre",
                    Description = "A refreshing cocktail.",
                    Ingredients = [null!]
                }
            ]
        };

        //Act & Assert
        Assert.Throws<ArgumentNullException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldThrowInvalidOperationException_WhenIngredientNameIsNull()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "Here is my recommendation.",
            Cocktails =
            [
                new CocktailDto
                {
                    Name = "Cuba Libre",
                    Description = "A refreshing cocktail.",
                    Ingredients =
                    [
                        new IngredientDto { Name = null!, Percent = 100 }
                    ]
                }
            ]
        };

        //Act & Assert
        Assert.Throws<InvalidOperationException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldThrowInvalidOperationException_WhenIngredientNameIsEmpty()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "Here is my recommendation.",
            Cocktails =
            [
                new CocktailDto
                {
                    Name = "Cuba Libre",
                    Description = "A refreshing cocktail.",
                    Ingredients =
                    [
                        new IngredientDto { Name = string.Empty, Percent = 100 }
                    ]
                }
            ]
        };

        //Act & Assert
        Assert.Throws<InvalidOperationException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldThrowInvalidOperationException_WhenIngredientNameContainsOnlyWhitespace()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "Here is my recommendation.",
            Cocktails =
            [
                new CocktailDto
                {
                    Name = "Cuba Libre",
                    Description = "A refreshing cocktail.",
                    Ingredients =
                    [
                        new IngredientDto { Name = "   ", Percent = 100 }
                    ]
                }
            ]
        };

        //Act & Assert
        Assert.Throws<InvalidOperationException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldThrowInvalidOperationException_WhenIngredientPercentIsZero()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "Here is my recommendation.",
            Cocktails =
            [
                new CocktailDto
                {
                    Name = "Cuba Libre",
                    Description = "A refreshing cocktail.",
                    Ingredients =
                    [
                        new IngredientDto { Name = "Vodka", Percent = 0 }
                    ]
                }
            ]
        };

        //Act & Assert
        Assert.Throws<InvalidOperationException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldThrowInvalidOperationException_WhenIngredientPercentIsNegative()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "Here is my recommendation.",
            Cocktails =
            [
                new CocktailDto
                {
                    Name = "Cuba Libre",
                    Description = "A refreshing cocktail.",
                    Ingredients =
                    [
                        new IngredientDto { Name = "Vodka", Percent = -10 }
                    ]
                }
            ]
        };

        //Act & Assert
        Assert.Throws<InvalidOperationException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldThrowInvalidOperationException_WhenIngredientPercentSumIsNot100()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "Here is my recommendation.",
            Cocktails =
            [
                new CocktailDto
                {
                    Name = "Cuba Libre",
                    Description = "A refreshing cocktail.",
                    Ingredients =
                    [
                        new IngredientDto { Name = "Vodka", Percent = 40 },
                        new IngredientDto { Name = "Orange Juice", Percent = 50 }
                    ]
                }
            ]
        };

        //Act & Assert
        Assert.Throws<InvalidOperationException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldThrowInvalidOperationException_WhenIngredientPercentSumExceeds100()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "Here is my recommendation.",
            Cocktails =
            [
                new CocktailDto
                {
                    Name = "Cuba Libre",
                    Description = "A refreshing cocktail.",
                    Ingredients =
                    [
                        new IngredientDto { Name = "Vodka", Percent = 60 },
                        new IngredientDto { Name = "Orange Juice", Percent = 50 }
                    ]
                }
            ]
        };

        //Act & Assert
        Assert.Throws<InvalidOperationException>(
            () => validationService.Validate(response));
    }

    [Fact]
    public void Validate_ShouldNotThrowException_WhenCocktailsListIsEmpty()
    {
        //Arrange
        var validationService = new CocktailValidationService();

        var response = new AssistantResponseDto
        {
            Reply = "The bartender can help you with that.",
            Cocktails = []
        };

        //Act & Assert
        validationService.Validate(response);
    }
}
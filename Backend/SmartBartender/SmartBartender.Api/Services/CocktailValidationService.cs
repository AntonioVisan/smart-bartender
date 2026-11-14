using SmartBartender.Api.DTOs;

namespace SmartBartender.Api.Services;

public class CocktailValidationService
{
    public void Validate(AssistantResponseDto response)
    {
        if (response == null)
        {
            throw new ArgumentNullException(nameof(response));
        }

        if (string.IsNullOrWhiteSpace(response.Reply))
        {
            throw new InvalidOperationException(
                "Assistant reply cannot be empty.");
        }

        if (response.Cocktails == null)
        {
            throw new InvalidOperationException(
                "Cocktails list cannot be null.");
        }

        foreach(var cocktail in response.Cocktails)
        {
            ValidateCocktail(cocktail);
        }
    }

    private void ValidateCocktail(CocktailDto cocktail)
    {
        if (cocktail == null)
        {
            throw new ArgumentNullException(nameof(cocktail));
        }

        if (string.IsNullOrWhiteSpace(cocktail.Name))
        {
            throw new InvalidOperationException(
                "Cocktail name cannot be empty.");
        }

        if (string.IsNullOrWhiteSpace(cocktail.Description))
        {
            throw new InvalidOperationException(
                "Cocktail description cannot be empty.");
        }

        ValidateIngredients(cocktail.Ingredients);
    }

    private void ValidateIngredients(List<IngredientDto> ingredients)
    {
        if (ingredients == null)
        {
            throw new ArgumentNullException(nameof(ingredients));
        }

        if (ingredients.Count == 0)
        {
            throw new InvalidOperationException(
                "Cocktail must contain at least one ingredient.");
        }

        long ingredientsPercent = 0;

        foreach(var ingredient in ingredients)
        {
            if (ingredient == null)
            {
                throw new ArgumentNullException(nameof(ingredient));
            }

            if (string.IsNullOrWhiteSpace(ingredient.Name))
            {
                throw new InvalidOperationException(
                    "Ingredient name cannot be empty.");
            }

            if (ingredient.Percent <= 0)
            {
                throw new InvalidOperationException(
                    "Ingredient percent must be greater than 0.");
            }

            ingredientsPercent += ingredient.Percent;
        }

        if(ingredientsPercent != 100)
        {
            throw new InvalidOperationException(
                "Sum of ingredients percent must be 100.");
        }
    }
}

using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;
using Microsoft.Extensions.Logging;

namespace Dotnetstore.OvenSimulator.Recipes.Services;

internal sealed class RecipeService
    (IRecipeRepository recipeRepository,
        ILogger<RecipeService> logger): IRecipeService
{
    async ValueTask<IEnumerable<RecipeResponse>> IRecipeService.GetAllAsync(CancellationToken ct)
    {
        var recipes = await recipeRepository.GetAllAsync(ct);
        
        logger.LogInformation("Retrieved {Count} recipes", recipes.Count);
        
        return recipes.Select(x => x.ToRecipeResponse());
    }
}
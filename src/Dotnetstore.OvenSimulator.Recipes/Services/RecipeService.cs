using Ardalis.Result;
using Dotnetstore.OvenSimulator.Contracts.Entities;
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

    async ValueTask<Result<RecipeResponse?>> IRecipeService.GetByIdAsync(Guid id, CancellationToken ct)
    {
        var recipe = await recipeRepository.GetByIdAsync(new RecipeId(id), ct);

        if (recipe is null)
        {
            logger.LogWarning("No recipe found with id {Id}", id.ToString());
            return Result<RecipeResponse?>.NotFound("No recipe found with id {Id}", id.ToString());
        }
        
        return Result<RecipeResponse?>.Success(recipe.ToRecipeResponse());
    }

    async ValueTask<Result<RecipeResponse?>> IRecipeService.GetByNameAsync(string name, CancellationToken ct)
    {
        var recipe = await recipeRepository.GetByNameAsync(name, ct);
        
        if (recipe is null)
        {
            logger.LogWarning("No recipe found with name {Name}", name);
            return Result<RecipeResponse?>.NotFound("No recipe found with name {Name}", name);
        }
        
        return Result<RecipeResponse?>.Success(recipe.ToRecipeResponse());
    }
}
using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;

namespace Dotnetstore.OvenSimulator.Recipes.Services;

internal interface IRecipeService
{
    ValueTask<IEnumerable<RecipeResponse>> GetAllAsync(CancellationToken ct);
}
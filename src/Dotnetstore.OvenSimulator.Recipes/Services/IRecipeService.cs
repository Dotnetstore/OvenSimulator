using Ardalis.Result;
using Dotnetstore.OvenSimulator.Contracts.Entities;
using Dotnetstore.OvenSimulator.SDK.Recipes.Requests;
using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;

namespace Dotnetstore.OvenSimulator.Recipes.Services;

internal interface IRecipeService
{
    ValueTask<IEnumerable<RecipeResponse>> GetAllAsync(CancellationToken ct);
    
    ValueTask<Result<RecipeResponse?>> GetByIdAsync(Guid id, CancellationToken ct);
    
    ValueTask<Result<RecipeResponse?>> GetByNameAsync(string name, CancellationToken ct);
    
    ValueTask<Result<RecipeResponse?>> CreateAsync(CreateRecipeRequest request, CancellationToken ct);
    
    ValueTask<Result<Recipe?>> UpdateAsync(UpdateRecipeRequest request, CancellationToken ct);
    
    ValueTask<Result<bool?>> DeleteAsync(Guid id, CancellationToken ct);
}
using Dotnetstore.OvenSimulator.Contracts.Entities;

namespace Dotnetstore.OvenSimulator.Recipes.Services;

public interface IRecipeRepository
{
    ValueTask<List<Recipe>> GetAllAsync(CancellationToken cancellationToken = default);
    
    ValueTask<Recipe?> GetByIdAsync(RecipeId id, CancellationToken cancellationToken = default);
    
    ValueTask<Recipe?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    
    void Create(Recipe recipe);
    
    void Update(Recipe recipe);
    
    void Delete(Recipe recipe);
    
    ValueTask SaveChangesAsync(CancellationToken cancellationToken = default);
}
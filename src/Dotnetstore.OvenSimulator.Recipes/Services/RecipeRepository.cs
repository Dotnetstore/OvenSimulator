using Dotnetstore.OvenSimulator.Contracts.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Dotnetstore.OvenSimulator.Recipes.Services;

internal sealed class RecipeRepository(
    IRecipeUnitOfWork unitOfWork,
    ILogger<RecipeRepository> logger) : IRecipeRepository
{
    private IQueryable<Recipe> GetRecipeQuery()
    {
        return unitOfWork
            .Repository<Recipe>()
            .Entities
            .AsNoTracking()
            .OrderBy(x => x.Name);
    }
    
    async ValueTask<List<Recipe>> IRecipeRepository.GetAllAsync(CancellationToken cancellationToken)
    {
        var query = GetRecipeQuery();
        
        logger.LogInformation("Query for all recipes {query}", query.ToQueryString());
        
        return await query
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    async ValueTask<Recipe?> IRecipeRepository.GetByIdAsync(RecipeId id, CancellationToken cancellationToken)
    {
        var query = GetRecipeQuery();
        
        query = query
            .Where(x => x.Id == id);
        
        logger.LogInformation("Query recipe by id {query}", query.ToQueryString());

        return await query
            .FirstOrDefaultAsync(cancellationToken);
    }

    async ValueTask<Recipe?> IRecipeRepository.GetByNameAsync(string name, CancellationToken cancellationToken)
    {
        var query = GetRecipeQuery();
            
        query = query
            .Where(x => x.Name == name);
        
        logger.LogInformation("Query recipe by name {query}", query.ToQueryString());

        return await query
            .FirstOrDefaultAsync(cancellationToken);
    }

    void IRecipeRepository.Create(Recipe recipe)
    {
        logger.LogInformation("Create recipe {@recipe}", recipe);
        unitOfWork
            .Repository<Recipe>()
            .Create(recipe);
    }

    void IRecipeRepository.Update(Recipe recipe)
    {
        logger.LogInformation("Update recipe {@recipe}", recipe);
        unitOfWork
            .Repository<Recipe>()
            .Update(recipe);
    }

    void IRecipeRepository.Delete(Recipe recipe)
    {
        logger.LogInformation("Delete recipe {@recipe}", recipe);
        unitOfWork
            .Repository<Recipe>()
            .Delete(recipe);
    }

    async ValueTask IRecipeRepository.SaveChangesAsync(CancellationToken cancellationToken)
    {
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
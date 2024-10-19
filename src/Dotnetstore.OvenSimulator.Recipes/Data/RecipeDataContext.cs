using Dotnetstore.OvenSimulator.Contracts.Entities;
using Dotnetstore.OvenSimulator.SharedKernel.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace Dotnetstore.OvenSimulator.Recipes.Data;

internal sealed class RecipeDataContext(DbContextOptions<RecipeDataContext> options) : ApiContext<RecipeDataContext>(options)
{
    internal DbSet<Recipe> Recipes => Set<Recipe>();
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.HasDefaultSchema("Recipe");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IRecipeAssemblyMarker).Assembly);
    }
}
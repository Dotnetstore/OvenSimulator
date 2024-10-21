using Dotnetstore.OvenSimulator.Recipes.Data;
using Dotnetstore.OvenSimulator.Recipes.Health;
using Dotnetstore.OvenSimulator.Recipes.Services;
using Dotnetstore.OvenSimulator.SharedKernel.Extensions;
using FastEndpoints;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Dotnetstore.OvenSimulator.Recipes.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddRecipes(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // mediatRAssemblies.Add((Assembly)typeof(IRecipeAssemblyMarker).Assembly);

        var connectionString = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");
        ArgumentException.ThrowIfNullOrWhiteSpace(connectionString, nameof(connectionString));

        services
            .AddFastEndpoints()
            .AddDbContext<RecipeDataContext>(connectionString)
            .AddScoped<IRecipeRepository, RecipeRepository>()
            .AddScoped<IRecipeService, RecipeService>()
            .AddScoped<IRecipeUnitOfWork, RecipeUnitOfWork>()
            .EnsureDbCreated<RecipeDataContext>();

        services
            .AddHealthChecks()
            .AddSqlServer(connectionString, healthQuery: "SELECT 1", name: "SQL Server",
                failureStatus: HealthStatus.Unhealthy)
            .AddCheck<DatabaseHealthCheck>("Recipe")
            .AddCheck<MemoryHealthCheck>("Memory Recipe");
        
        services
            .AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(10);
                opt.MaximumHistoryEntriesPerEndpoint(60);
                opt.SetApiMaxActiveRequests(1);
                opt.AddHealthCheckEndpoint("Recipe", "/health");
            })
            .AddInMemoryStorage();
        
        return services;
    }
}
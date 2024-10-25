using Dotnetstore.OvenSimulator.Recipes.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace Dotnetstore.OvenSimulator.Recipes.Health;

internal sealed class DatabaseHealthCheck(
    RecipeDataContext ovenDataContext,
    ILogger<DatabaseHealthCheck> logger) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new())
    {
        try
        {
            await ovenDataContext
                .Recipes
                .OrderBy(x => x.Name)
                .FirstOrDefaultAsync(cancellationToken);
            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Database health check failed");
            return HealthCheckResult.Unhealthy(exception: ex);
        }
    }
}
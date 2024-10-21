using Dotnetstore.OvenSimulator.Recipes.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Dotnetstore.OvenSimulator.Recipes.Health;

internal sealed class DatabaseHealthCheck(RecipeDataContext ovenDataContext) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new())
    {
        try
        {
            await ovenDataContext.Recipes.FirstOrDefaultAsync(cancellationToken);
            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(exception: ex);
        }
    }
}
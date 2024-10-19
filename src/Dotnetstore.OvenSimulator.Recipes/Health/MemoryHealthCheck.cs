using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace Dotnetstore.OvenSimulator.Recipes.Health;

internal sealed class MemoryHealthCheck(IOptionsMonitor<MemoryCheckOptions> options) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
    {
        var memOptions = options.Get(context.Registration.Name);
        
        var allocated = GC.GetTotalMemory(forceFullCollection: false);
        var data = new Dictionary<string, object>()
        {
            { "AllocatedBytes", allocated },
            { "Gen0Collections", GC.CollectionCount(0) },
            { "Gen1Collections", GC.CollectionCount(1) },
            { "Gen2Collections", GC.CollectionCount(2) },
        };
        
        var status = allocated < memOptions.Threshold ? HealthStatus.Healthy : HealthStatus.Unhealthy;
        
        return await Task.FromResult(new HealthCheckResult(
            status,
            description: "Reports degraded status if allocated bytes " +
                         $">= {memOptions.Threshold} bytes.",
            exception: null,
            data: data));
    }
}

internal sealed class MemoryCheckOptions
{
    public string MemoryStatus { get; set; } = null!;
    
    public long Threshold { get; set; } = 1024L * 1024L * 1024L;
}
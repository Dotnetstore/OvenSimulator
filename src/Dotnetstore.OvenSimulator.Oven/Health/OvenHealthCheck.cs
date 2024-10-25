using Dotnetstore.OvenSimulator.Oven.Services;
using Dotnetstore.OvenSimulator.SDK.Oven;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;

namespace Dotnetstore.OvenSimulator.Oven.Health;

internal sealed class OvenHealthCheck(
    IOvenSimulator ovenSimulator,
    ILogger<OvenHealthCheck> logger) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new())
    {
        await Task.CompletedTask;
        
        try
        {
            if (ovenSimulator.CurrentError == OvenErrorType.HeaterFailure)
                return OvenUnhealthy("Heater failure");
            
            if (ovenSimulator.CurrentError == OvenErrorType.ThermostatIssue)
                return OvenUnhealthy("Thermostat issue");
            
            if (ovenSimulator.CurrentError == OvenErrorType.GradualHeaterFailure)
                return OvenDegraded("Gradual heater failure");
            
            if (ovenSimulator.CurrentError == OvenErrorType.IntermittentSensorFailure)
                return OvenDegraded("Intermittent sensor failure");

            if (ovenSimulator.ActiveRecipe is not null)
            {
                if (ovenSimulator.CurrentTemperature < ovenSimulator.TargetTemperature - 10)
                    return OvenDegraded("Oven is not hot enough");

                if (ovenSimulator.CurrentTemperature > ovenSimulator.TargetTemperature + 10)
                    return OvenDegraded("Oven is too hot");
            }
            
            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return OvenUnhealthy($"Exception occurred while checking oven health {ex.Message}");
        }
    }
    
    private HealthCheckResult OvenUnhealthy(string ovenStatus)
    {
        logger.LogError("Oven status: {OvenStatus}", ovenStatus);
        return HealthCheckResult.Unhealthy(ovenStatus);
    }
    
    private HealthCheckResult OvenDegraded(string ovenStatus)
    {
        logger.LogWarning("Oven status: {OvenStatus}", ovenStatus);
        return HealthCheckResult.Degraded(ovenStatus);
    }
}
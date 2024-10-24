using Dotnetstore.OvenSimulator.Oven.Services;
using Dotnetstore.OvenSimulator.SDK.Oven;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Dotnetstore.OvenSimulator.Oven.Health;

internal sealed class OvenHealthCheck(IOvenSimulator ovenSimulator) : IHealthCheck
{
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new())
    {
        await Task.CompletedTask;
        
        try
        {
            if (ovenSimulator.CurrentError == OvenErrorType.HeaterFailure)
                return HealthCheckResult.Unhealthy("Heater failure");
            
            if (ovenSimulator.CurrentError == OvenErrorType.ThermostatIssue)
                return HealthCheckResult.Unhealthy("Thermostat issue");
            
            if (ovenSimulator.CurrentError == OvenErrorType.GradualHeaterFailure)
                return HealthCheckResult.Degraded("Gradual heater failure");
            
            if (ovenSimulator.CurrentError == OvenErrorType.IntermittentSensorFailure)
                return HealthCheckResult.Degraded("Intermittent sensor failure");

            if (ovenSimulator.ActiveRecipe is not null)
            {
                if (ovenSimulator.CurrentTemperature < ovenSimulator.TargetTemperature - 10)
                    return HealthCheckResult.Degraded("Oven is not hot enough");
            
                if (ovenSimulator.CurrentTemperature > ovenSimulator.TargetTemperature + 10)
                    return HealthCheckResult.Degraded("Oven is too hot");
            }
            
            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(exception: ex);
        }
    }
}
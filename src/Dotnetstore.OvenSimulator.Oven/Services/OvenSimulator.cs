using Dotnetstore.OvenSimulator.SDK.Oven;
using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;
using Microsoft.Extensions.Configuration;

namespace Dotnetstore.OvenSimulator.Oven.Services;

internal sealed class OvenSimulator : IOvenSimulator
{
    public RecipeResponse? ActiveRecipe { get; set; }

    public double CurrentTemperature { get; set; }

    public double AmbientTemperature { get; set; }

    public double TargetTemperature { get; set; }
    public double HeatCapacity { get; set; }
    public double HeatLossCoefficient { get; set; }

    public double HeaterPowerPercentage { get; set; }
    public bool HeatingElementOn { get; set; }
    public OvenErrorType CurrentError { get; set; } = OvenErrorType.None;
    
    private readonly Random _random = new();

    public OvenSimulator(IConfiguration configuration)
    {
        AmbientTemperature = configuration.GetValue<double>("Oven:AmbientTemperature");
        CurrentTemperature = configuration.GetValue<double>("Oven:AmbientTemperature");
    }

    void IOvenSimulator.SimulateError(OvenErrorType error)
    {
        CurrentError = error;
    }
    
    // Method to simulate using the Runge-Kutta 4th order method
    void IOvenSimulator.RungeKuttaStep(double deltaTime)
    {
        RungeKuttaStep(deltaTime);
    }
    
    private void RungeKuttaStep(double deltaTime)
    {
        if (CurrentError == OvenErrorType.None)
        {
            StandardTemperatureSimulation(deltaTime);
        }
        else
        {
            switch (CurrentError)
            {
                case OvenErrorType.HeaterFailure:
                    HeaterPowerPercentage = 0.0;  // Heater stops working
                    StandardTemperatureSimulation(deltaTime);
                    break;

                case OvenErrorType.GradualHeaterFailure:
                    GradualHeaterFailureSimulation(deltaTime);
                    break;

                case OvenErrorType.IntermittentSensorFailure:
                    IntermittentSensorFailureSimulation(deltaTime);
                    break;

                case OvenErrorType.ThermostatIssue:
                    ThermostatIssueSimulation(deltaTime);
                    break;
            }
        }
    }

    private void StandardTemperatureSimulation(double deltaTime)
    {
        // Runge-Kutta 4th order method to update temperature
        var k1 = deltaTime * HeatingRate(CurrentTemperature);
        var k2 = deltaTime * HeatingRate(CurrentTemperature + k1 / 2);
        var k3 = deltaTime * HeatingRate(CurrentTemperature + k2 / 2);
        var k4 = deltaTime * HeatingRate(CurrentTemperature + k3);

        // Update temperature
        CurrentTemperature += (k1 + 2 * k2 + 2 * k3 + k4) / 6;

        // Check if heating element should be turned off
        if (CurrentTemperature >= TargetTemperature)
        {
            HeatingElementOn = false;
            CurrentTemperature = TargetTemperature;
        }

        return;

        // Define the function for dT/dt
        double HeatingRate(double T) => HeaterPowerPercentage / 100.0 * (10000.0 / HeatCapacity) - HeatLossCoefficient * (T - AmbientTemperature);
    }
    
    // Gradual Heater Failure: Heater power decreases over time
    private void GradualHeaterFailureSimulation(double deltaTime)
    {
        HeaterPowerPercentage = Math.Max(HeaterPowerPercentage - (0.1 * deltaTime), 0.0);
        StandardTemperatureSimulation(deltaTime);
    }
    
    // Intermittent Sensor Failure: Random temperature spikes/drops
    private void IntermittentSensorFailureSimulation(double deltaTime)
    {
        if (_random.NextDouble() < 0.1)  // 10% chance of a sensor failure each step
        {
            // Introduce random fluctuation in the temperature reading
            var fluctuation = _random.NextDouble() * 50.0 - 25.0;  // Between -25 and +25 degrees
            CurrentTemperature += fluctuation;
        }
        else
        {
            StandardTemperatureSimulation(deltaTime);
        }
    }
    
    // Thermostat Issue: Incorrectly reported target temperature
    private void ThermostatIssueSimulation(double deltaTime)
    {
        var originalTarget = TargetTemperature;

        // Simulate incorrect thermostat reading (e.g., off by 20 degrees)
        TargetTemperature += _random.NextDouble() > 0.5 ? 20.0 : -20.0;

        StandardTemperatureSimulation(deltaTime);

        // Restore the correct target temperature after calculation
        TargetTemperature = originalTarget;
    }

    void IOvenSimulator.StartHeating()
    {
        if (ActiveRecipe is null)
            ArgumentException.ThrowIfNullOrEmpty("No recipe loaded");
        
        TargetTemperature = ActiveRecipe!.Value.TargetTemperature;
        HeatCapacity = ActiveRecipe.Value.HeatCapacity;
        HeatLossCoefficient = ActiveRecipe.Value.HeatLossCoefficient;
        HeaterPowerPercentage = ActiveRecipe.Value.HeaterPowerPercentage;
        
        HeatingElementOn = true;
    }
    
    void IOvenSimulator.StopHeating()
    {
        HeatingElementOn = false;
    }
    
    void IOvenSimulator.Update(double deltaTime)
    {
        if (HeatingElementOn)
        {
            RungeKuttaStep(deltaTime);
        }
        else
        {
            // Cooldown with heat loss
            CurrentTemperature -= HeatLossCoefficient * (CurrentTemperature - AmbientTemperature) * deltaTime;
            if (CurrentTemperature < AmbientTemperature)
            {
                CurrentTemperature = AmbientTemperature;
            }
        }
    }
}
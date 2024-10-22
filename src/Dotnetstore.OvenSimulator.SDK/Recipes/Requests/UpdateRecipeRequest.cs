namespace Dotnetstore.OvenSimulator.SDK.Recipes.Requests;

public record struct UpdateRecipeRequest(
    Guid Id,
    string Name,
    double HeatCapacity,
    double HeatLossCoefficient,
    double HeaterPowerPercentage,
    double TargetTemperature);
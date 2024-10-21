namespace Dotnetstore.OvenSimulator.SDK.Recipes.Requests;

public record struct CreateRecipeRequest(
    string Name,
    double HeatCapacity,
    double HeatLossCoefficient,
    double HeaterPowerPercentage,
    double TargetTemperature);
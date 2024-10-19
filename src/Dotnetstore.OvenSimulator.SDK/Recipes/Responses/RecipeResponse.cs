namespace Dotnetstore.OvenSimulator.SDK.Recipes.Responses;

public record struct RecipeResponse(
    Guid Id,
    string Name,
    double HeatCapacity,
    double HeatLossCoefficient,
    double HeaterPowerPercentage,
    double TargetTemperature);
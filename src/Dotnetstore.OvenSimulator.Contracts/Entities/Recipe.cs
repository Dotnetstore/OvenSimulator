using Dotnetstore.OvenSimulator.SharedKernel.Models;

namespace Dotnetstore.OvenSimulator.Contracts.Entities;

public class Recipe : BaseAuditableEntity
{
    public required RecipeId Id { get; init; }

    public required string Name { get; set; }

    public required double HeatCapacity { get; set; }

    public required double HeatLossCoefficient { get; set; }

    public required double HeaterPowerPercentage { get; set; }
    
    public required double TargetTemperature { get; set; }
}

public record struct RecipeId(Guid Value);
using Ardalis.GuardClauses;
using Dotnetstore.OvenSimulator.Contracts.Entities;
using Dotnetstore.OvenSimulator.SharedKernel.Services;

namespace Dotnetstore.OvenSimulator.Recipes.GetAll;

internal sealed class RecipeBuilder
{
    private RecipeId RecipeId { get; set; }
    private string RecipeName { get; set; } = null!;
    private double HeatCapacity { get; set; }
    private double HeatLossCoefficient { get; set; }
    private double HeaterPowerPercentage { get; set; }
    private double TargetTemperature { get; set; }
    
    internal static RecipeBuilder Create()
    {
        return new RecipeBuilder();
    }
    
    internal RecipeBuilder WithRecipeId(RecipeId recipeId)
    {
        RecipeId = recipeId;
        return this;
    }
    
    internal RecipeBuilder WithRecipeName(string recipeName)
    {
        var name = Guard.Against.NullOrEmpty(recipeName, nameof(RecipeName));
        name = Guard.Against.StringTooLong(name, DataSchemeConstants.NameMaxLength, nameof(RecipeName));
        RecipeName = name;
        return this;
    }
    
    internal RecipeBuilder WithHeatCapacity(double heatCapacity)
    {
        var capacity = Guard.Against.NegativeOrZero(heatCapacity, nameof(HeatCapacity));
        capacity = Guard.Against.OutOfRange(capacity, nameof(HeatCapacity), 0.0, 10000.0);
        HeatCapacity = capacity;
        return this;
    }
    
    internal RecipeBuilder WithHeatLossCoefficient(double heatLossCoefficient)
    {
        var coefficient = Guard.Against.NegativeOrZero(heatLossCoefficient, nameof(HeatLossCoefficient));
        coefficient = Guard.Against.OutOfRange(coefficient, nameof(HeatLossCoefficient), 0.0, 0.9);
        HeatLossCoefficient = coefficient;
        return this;
    }
    
    internal RecipeBuilder WithHeaterPowerPercentage(double heaterPowerPercentage)
    {
        var percentage = Guard.Against.NegativeOrZero(heaterPowerPercentage, nameof(HeaterPowerPercentage));
        percentage = Guard.Against.OutOfRange(percentage, nameof(HeaterPowerPercentage), 0.0, 100.0);
        HeaterPowerPercentage = percentage;
        return this;
    }
    
    internal RecipeBuilder WithTargetTemperature(double targetTemperature)
    {
        var temperature = Guard.Against.NegativeOrZero(targetTemperature, nameof(TargetTemperature));
        temperature = Guard.Against.OutOfRange(temperature, nameof(TargetTemperature), -273.15, 1000.0);
        TargetTemperature = temperature;
        return this;
    }
    
    internal Recipe Build()
    {
        return new Recipe
        {
            Id = RecipeId,
            Name = RecipeName,
            HeatCapacity = HeatCapacity,
            HeatLossCoefficient = HeatLossCoefficient,
            HeaterPowerPercentage = HeaterPowerPercentage,
            TargetTemperature = TargetTemperature
        };
    }
}
using Dotnetstore.OvenSimulator.Contracts.Entities;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Dotnetstore.OvenSimulator.Contracts.Tests.Entities;

public class RecipeTests
{
    [Fact]
    public void Recipe_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var id = new RecipeId(Guid.NewGuid());
        const string name = "Test Recipe";
        const double heatCapacity = 100.0;
        const double heatLossCoefficient = 0.5;
        const double heaterPowerPercentage = 75.0;
        const double targetTemperature = 200.0;

        // Act
        var recipe = new Recipe
        {
            Id = id,
            Name = name,
            HeatCapacity = heatCapacity,
            HeatLossCoefficient = heatLossCoefficient,
            HeaterPowerPercentage = heaterPowerPercentage,
            TargetTemperature = targetTemperature
        };

        // Assert
        using (new AssertionScope())
        {
            recipe.Id.Should().Be(id);
            recipe.Name.Should().Be(name);
            recipe.HeatCapacity.Should().Be(heatCapacity);
            recipe.HeatLossCoefficient.Should().Be(heatLossCoefficient);
            recipe.HeaterPowerPercentage.Should().Be(heaterPowerPercentage);
            recipe.TargetTemperature.Should().Be(targetTemperature);
        }
    }

    [Fact]
    public void Recipe_ShouldBeEqual_WhenPropertiesAreSame()
    {
        // Arrange
        var id = new RecipeId(Guid.NewGuid());
        const string name = "Test Recipe";
        const double heatCapacity = 100.0;
        const double heatLossCoefficient = 0.5;
        const double heaterPowerPercentage = 75.0;
        const double targetTemperature = 200.0;

        var recipe1 = new Recipe
        {
            Id = id,
            Name = name,
            HeatCapacity = heatCapacity,
            HeatLossCoefficient = heatLossCoefficient,
            HeaterPowerPercentage = heaterPowerPercentage,
            TargetTemperature = targetTemperature
        };

        var recipe2 = new Recipe
        {
            Id = id,
            Name = name,
            HeatCapacity = heatCapacity,
            HeatLossCoefficient = heatLossCoefficient,
            HeaterPowerPercentage = heaterPowerPercentage,
            TargetTemperature = targetTemperature
        };

        // Act & Assert
        recipe1.Should().BeEquivalentTo(recipe2);
    }

    [Fact]
    public void Recipe_ShouldNotBeEqual_WhenPropertiesAreDifferent()
    {
        // Arrange
        var recipe1 = new Recipe
        {
            Id = new RecipeId(Guid.NewGuid()),
            Name = "Test Recipe 1",
            HeatCapacity = 100.0,
            HeatLossCoefficient = 0.5,
            HeaterPowerPercentage = 75.0,
            TargetTemperature = 200.0
        };

        var recipe2 = new Recipe
        {
            Id = new RecipeId(Guid.NewGuid()),
            Name = "Test Recipe 2",
            HeatCapacity = 150.0,
            HeatLossCoefficient = 0.7,
            HeaterPowerPercentage = 80.0,
            TargetTemperature = 250.0
        };

        // Act & Assert
        recipe1.Should().NotBeEquivalentTo(recipe2);
    }
}
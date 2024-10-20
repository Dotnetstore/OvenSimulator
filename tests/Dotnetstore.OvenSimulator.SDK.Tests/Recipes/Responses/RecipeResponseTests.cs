using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Dotnetstore.OvenSimulator.SDK.Tests.Recipes.Responses;

public class RecipeResponseTests
{
    [Fact]
    public void RecipeResponse_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();
        const string name = "Test Recipe";
        const double heatCapacity = 100.0;
        const double heatLossCoefficient = 0.5;
        const double heaterPowerPercentage = 75.0;
        const double targetTemperature = 200.0;

        // Act
        var recipeResponse = new RecipeResponse(
            id,
            name,
            heatCapacity,
            heatLossCoefficient,
            heaterPowerPercentage,
            targetTemperature);

        // Assert
        using (new AssertionScope())
        {
            recipeResponse.Id.Should().Be(id);
            recipeResponse.Name.Should().Be(name);
            recipeResponse.HeatCapacity.Should().Be(heatCapacity);
            recipeResponse.HeatLossCoefficient.Should().Be(heatLossCoefficient);
            recipeResponse.HeaterPowerPercentage.Should().Be(heaterPowerPercentage);
            recipeResponse.TargetTemperature.Should().Be(targetTemperature);
        }
    }

    [Fact]
    public void RecipeResponse_ShouldBeEqual_WhenPropertiesAreSame()
    {
        // Arrange
        var id = Guid.NewGuid();
        const string name = "Test Recipe";
        const double heatCapacity = 100.0;
        const double heatLossCoefficient = 0.5;
        const double heaterPowerPercentage = 75.0;
        const double targetTemperature = 200.0;

        var recipeResponse1 = new RecipeResponse(
            id,
            name,
            heatCapacity,
            heatLossCoefficient,
            heaterPowerPercentage,
            targetTemperature);

        var recipeResponse2 = new RecipeResponse(
            id,
            name,
            heatCapacity,
            heatLossCoefficient,
            heaterPowerPercentage,
            targetTemperature);

        // Act & Assert
        recipeResponse1.Should().Be(recipeResponse2);
    }

    [Fact]
    public void RecipeResponse_ShouldNotBeEqual_WhenPropertiesAreDifferent()
    {
        // Arrange
        var recipeResponse1 = new RecipeResponse(
            Guid.NewGuid(),
            "Test Recipe 1",
            100.0,
            0.5,
            75.0,
            200.0);

        var recipeResponse2 = new RecipeResponse(
            Guid.NewGuid(),
            "Test Recipe 2",
            150.0,
            0.7,
            80.0,
            250.0);

        // Act & Assert
        recipeResponse1.Should().NotBe(recipeResponse2);
    }
}
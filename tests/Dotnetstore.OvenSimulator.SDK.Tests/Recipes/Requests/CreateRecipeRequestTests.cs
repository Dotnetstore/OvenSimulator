using Dotnetstore.OvenSimulator.SDK.Recipes.Requests;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Dotnetstore.OvenSimulator.SDK.Tests.Recipes.Requests;

public class CreateRecipeRequestTests
{
    [Fact]
    public void CreateRecipeRequest_ShouldHaveCorrectProperties()
    {
        // Arrange
        var request = new CreateRecipeRequest(
            "Test Recipe",
            1.0,
            2.0,
            3.0,
            4.0);

        // Act
        var name = request.Name;
        var heatCapacity = request.HeatCapacity;
        var heatLossCoefficient = request.HeatLossCoefficient;
        var heaterPowerPercentage = request.HeaterPowerPercentage;
        var targetTemperature = request.TargetTemperature;

        // Assert
        using (new AssertionScope())
        {
            name.Should().Be("Test Recipe");
            heatCapacity.Should().Be(1.0);
            heatLossCoefficient.Should().Be(2.0);
            heaterPowerPercentage.Should().Be(3.0);
            targetTemperature.Should().Be(4.0);
        }
    }
}
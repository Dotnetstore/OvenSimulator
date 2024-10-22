using Dotnetstore.OvenSimulator.SDK.Recipes.Requests;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Dotnetstore.OvenSimulator.SDK.Tests.Recipes.Requests;

public class UpdateRecipeRequestTests
{
    [Fact]
    public void UpdateRecipeRequest_ShouldHaveProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "Recipe Name";
        var heatCapacity = 1.0;
        var heatLossCoefficient = 2.0;
        var heaterPowerPercentage = 3.0;
        var targetTemperature = 4.0;

        // Act
        var request = new UpdateRecipeRequest(
            id,
            name,
            heatCapacity,
            heatLossCoefficient,
            heaterPowerPercentage,
            targetTemperature);

        // Assert
        using (new AssertionScope())
        {
            request.Id.Should().Be(id);
            request.Name.Should().Be(name);
            request.HeatCapacity.Should().Be(heatCapacity);
            request.HeatLossCoefficient.Should().Be(heatLossCoefficient);
            request.HeaterPowerPercentage.Should().Be(heaterPowerPercentage);
            request.TargetTemperature.Should().Be(targetTemperature);
        }
    }
}
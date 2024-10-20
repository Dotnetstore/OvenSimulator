using Dotnetstore.OvenSimulator.Contracts.Entities;
using Dotnetstore.OvenSimulator.Recipes.Services;
using FluentAssertions;
using Xunit;

namespace Dotnetstore.OvenSimulator.Recipes.Tests.Services;

public class RecipeMappersTests
{
    [Fact]
    public void ToRecipeResponse_ShouldMapRecipeToRecipeResponse()
    {
        // Arrange
        var recipeId = new RecipeId(Guid.NewGuid());
        var recipe = new Recipe
        {
            Id = recipeId,
            Name = "Test Recipe",
            HeatCapacity = 100.0,
            HeatLossCoefficient = 0.5,
            HeaterPowerPercentage = 75.0,
            TargetTemperature = 200.0
        };

        // Act
        var response = recipe.ToRecipeResponse();

        // Assert
        response.Should().NotBeNull();
        response.Id.Should().Be(recipeId.Value);
        response.Name.Should().Be("Test Recipe");
        response.HeatCapacity.Should().Be(100.0);
        response.HeatLossCoefficient.Should().Be(0.5);
        response.HeaterPowerPercentage.Should().Be(75.0);
        response.TargetTemperature.Should().Be(200.0);
    }
}
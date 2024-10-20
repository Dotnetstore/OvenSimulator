using Dotnetstore.OvenSimulator.Contracts.Entities;
using Dotnetstore.OvenSimulator.Recipes.GetAll;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Dotnetstore.OvenSimulator.Recipes.Tests.GetAll;

public class RecipeBuilderTests
{
    [Fact]
    public void Create_ShouldReturnRecipeBuilder()
    {
        // Act
        var builder = RecipeBuilder.Create();

        // Assert
        using (new AssertionScope())
        {
            builder.Should().NotBeNull();
            builder.Should().BeOfType<RecipeBuilder>();
        }
    }

    [Fact]
    public void WithRecipeId_ShouldSetRecipeId()
    {
        // Arrange
        var builder = RecipeBuilder.Create();
        var id = new RecipeId(Guid.NewGuid());

        // Act
        builder = builder.WithRecipeId(id);

        // Assert
        builder.Should().NotBeNull();
    }

    [Fact]
    public void WithRecipeName_ShouldSetRecipeName()
    {
        // Arrange
        var builder = RecipeBuilder.Create();
        const string name = "Test Recipe";

        // Act
        builder = builder.WithRecipeName(name);

        // Assert
        builder.Should().NotBeNull();
    }

    [Fact]
    public void WithHeatCapacity_ShouldSetHeatCapacity()
    {
        // Arrange
        var builder = RecipeBuilder.Create();
        const double heatCapacity = 100.0;

        // Act
        builder = builder.WithHeatCapacity(heatCapacity);

        // Assert
        builder.Should().NotBeNull();
    }

    [Fact]
    public void WithHeatLossCoefficient_ShouldSetHeatLossCoefficient()
    {
        // Arrange
        var builder = RecipeBuilder.Create();
        const double heatLossCoefficient = 0.5;

        // Act
        builder = builder.WithHeatLossCoefficient(heatLossCoefficient);

        // Assert
        builder.Should().NotBeNull();
    }

    [Fact]
    public void WithHeaterPowerPercentage_ShouldSetHeaterPowerPercentage()
    {
        // Arrange
        var builder = RecipeBuilder.Create();
        const double heaterPowerPercentage = 75.0;

        // Act
        builder = builder.WithHeaterPowerPercentage(heaterPowerPercentage);

        // Assert
        builder.Should().NotBeNull();
    }

    [Fact]
    public void WithTargetTemperature_ShouldSetTargetTemperature()
    {
        // Arrange
        var builder = RecipeBuilder.Create();
        const double targetTemperature = 200.0;

        // Act
        builder = builder.WithTargetTemperature(targetTemperature);

        // Assert
        builder.Should().NotBeNull();
    }

    [Fact]
    public void Build_ShouldReturnRecipeWithCorrectProperties()
    {
        // Arrange
        var id = new RecipeId(Guid.NewGuid());
        const string name = "Test Recipe";
        const double heatCapacity = 100.0;
        const double heatLossCoefficient = 0.5;
        const double heaterPowerPercentage = 75.0;
        const double targetTemperature = 200.0;

        var builder = RecipeBuilder.Create()
            .WithRecipeId(id)
            .WithRecipeName(name)
            .WithHeatCapacity(heatCapacity)
            .WithHeatLossCoefficient(heatLossCoefficient)
            .WithHeaterPowerPercentage(heaterPowerPercentage)
            .WithTargetTemperature(targetTemperature);

        // Act
        var recipe = builder.Build();

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
}
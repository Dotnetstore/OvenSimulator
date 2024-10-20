using Dotnetstore.OvenSimulator.Recipes.Create;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Dotnetstore.OvenSimulator.Recipes.Tests.Create;

public class CreateRecipeBuilderTests
{
    [Fact]
    public void CreateNewRecipe_ShouldReturnICreateRecipeId()
    {
        // Act
        var builder = CreateRecipeBuilder.CreateNewRecipe();

        // Assert
        builder.Should().BeAssignableTo<ICreateRecipeId>();
    }

    [Fact]
    public void CreateRecipeId_ShouldSetRecipeIdAndReturnISetRecipeNameObject()
    {
        // Arrange
        var builder = CreateRecipeBuilder.CreateNewRecipe();
        var id = Guid.NewGuid();

        // Act
        var result = builder.CreateRecipeId(id);

        // Assert
        result.Should().BeAssignableTo<ISetRecipeNameObject>();
    }

    [Fact]
    public void SetRecipeName_ShouldSetRecipeNameAndReturnISetRecipeHeatCapacityObject()
    {
        // Arrange
        var builder = CreateRecipeBuilder.CreateNewRecipe()
            .CreateRecipeId(Guid.NewGuid());
        const string name = "Test Recipe";

        // Act
        var result = builder.SetRecipeName(name);

        // Assert
        result.Should().BeAssignableTo<ISetRecipeHeatCapacityObject>();
    }

    [Fact]
    public void SetRecipeHeatCapacity_ShouldSetHeatCapacityAndReturnISetRecipeHeatLossCoefficientObject()
    {
        // Arrange
        var builder = CreateRecipeBuilder.CreateNewRecipe()
            .CreateRecipeId(Guid.NewGuid())
            .SetRecipeName("Test Recipe");
        const double heatCapacity = 100.0;

        // Act
        var result = builder.SetRecipeHeatCapacity(heatCapacity);

        // Assert
        result.Should().BeAssignableTo<ISetRecipeHeatLossCoefficientObject>();
    }

    [Fact]
    public void SetRecipeHeatLossCoefficient_ShouldSetHeatLossCoefficientAndReturnISetRecipeHeaterPowerPercentageObject()
    {
        // Arrange
        var builder = CreateRecipeBuilder.CreateNewRecipe()
            .CreateRecipeId(Guid.NewGuid())
            .SetRecipeName("Test Recipe")
            .SetRecipeHeatCapacity(100.0);
        const double heatLossCoefficient = 0.5;

        // Act
        var result = builder.SetRecipeHeatLossCoefficient(heatLossCoefficient);

        // Assert
        result.Should().BeAssignableTo<ISetRecipeHeaterPowerPercentageObject>();
    }

    [Fact]
    public void SetRecipeHeaterPowerPercentage_ShouldSetHeaterPowerPercentageAndReturnISetRecipeTargetTemperatureObject()
    {
        // Arrange
        var builder = CreateRecipeBuilder.CreateNewRecipe()
            .CreateRecipeId(Guid.NewGuid())
            .SetRecipeName("Test Recipe")
            .SetRecipeHeatCapacity(100.0)
            .SetRecipeHeatLossCoefficient(0.5);
        const double heaterPowerPercentage = 75.0;

        // Act
        var result = builder.SetRecipeHeaterPowerPercentage(heaterPowerPercentage);

        // Assert
        result.Should().BeAssignableTo<ISetRecipeTargetTemperatureObject>();
    }

    [Fact]
    public void SetRecipeTargetTemperature_ShouldSetTargetTemperatureAndReturnIBuildRecipe()
    {
        // Arrange
        var builder = CreateRecipeBuilder.CreateNewRecipe()
            .CreateRecipeId(Guid.NewGuid())
            .SetRecipeName("Test Recipe")
            .SetRecipeHeatCapacity(300.0)
            .SetRecipeHeatLossCoefficient(0.5)
            .SetRecipeHeaterPowerPercentage(75.0);
        const double targetTemperature = 200.0;

        // Act
        var result = builder.SetRecipeTargetTemperature(targetTemperature);

        // Assert
        result.Should().BeAssignableTo<IBuildRecipe>();
    }

    [Fact]
    public void Build_ShouldReturnRecipeWithCorrectProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        const string name = "Test Recipe";
        const double heatCapacity = 300.0;
        const double heatLossCoefficient = 0.5;
        const double heaterPowerPercentage = 75.0;
        const double targetTemperature = 200.0;

        var builder = CreateRecipeBuilder.CreateNewRecipe()
            .CreateRecipeId(id)
            .SetRecipeName(name)
            .SetRecipeHeatCapacity(heatCapacity)
            .SetRecipeHeatLossCoefficient(heatLossCoefficient)
            .SetRecipeHeaterPowerPercentage(heaterPowerPercentage)
            .SetRecipeTargetTemperature(targetTemperature);

        // Act
        var recipe = builder.Build();

        // Assert
        using (new AssertionScope())
        {
            recipe.Id.Value.Should().Be(id);
            recipe.Name.Should().Be(name);
            recipe.HeatCapacity.Should().Be(heatCapacity);
            recipe.HeatLossCoefficient.Should().Be(heatLossCoefficient);
            recipe.HeaterPowerPercentage.Should().Be(heaterPowerPercentage);
            recipe.TargetTemperature.Should().Be(targetTemperature);
        }
    }
}
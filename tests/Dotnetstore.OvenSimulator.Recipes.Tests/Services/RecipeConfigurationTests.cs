using Dotnetstore.OvenSimulator.Contracts.Entities;
using Dotnetstore.OvenSimulator.Recipes.Services;
using Dotnetstore.OvenSimulator.SharedKernel.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Dotnetstore.OvenSimulator.Recipes.Tests.Services;

public class RecipeConfigurationTests
{
    [Fact]
    public void Configure_ShouldSetUpEntityConfiguration()
    {
        // Arrange
        var builder = new ModelBuilder();
        var entityBuilder = builder.Entity<Recipe>();
        var configuration = new RecipeConfiguration();

        // Act
        configuration.Configure(entityBuilder);

        // Assert
        var entityType = builder.Model.FindEntityType(typeof(Recipe));
        entityType.Should().NotBeNull();

        var idProperty = entityType!.FindProperty(nameof(Recipe.Id));
        idProperty.Should().NotBeNull();
        idProperty!.IsPrimaryKey().Should().BeTrue();
        idProperty.IsNullable.Should().BeFalse();

        var nameProperty = entityType.FindProperty(nameof(Recipe.Name));
        nameProperty.Should().NotBeNull();
        nameProperty!.IsNullable.Should().BeFalse();
        nameProperty.GetMaxLength().Should().Be(DataSchemeConstants.NameMaxLength);

        var heatCapacityProperty = entityType.FindProperty(nameof(Recipe.HeatCapacity));
        heatCapacityProperty.Should().NotBeNull();
        heatCapacityProperty!.IsNullable.Should().BeFalse();

        var heatLossCoefficientProperty = entityType.FindProperty(nameof(Recipe.HeatLossCoefficient));
        heatLossCoefficientProperty.Should().NotBeNull();
        heatLossCoefficientProperty!.IsNullable.Should().BeFalse();

        var heaterPowerPercentageProperty = entityType.FindProperty(nameof(Recipe.HeaterPowerPercentage));
        heaterPowerPercentageProperty.Should().NotBeNull();
        heaterPowerPercentageProperty!.IsNullable.Should().BeFalse();

        var targetTemperatureProperty = entityType.FindProperty(nameof(Recipe.TargetTemperature));
        targetTemperatureProperty.Should().NotBeNull();
        targetTemperatureProperty!.IsNullable.Should().BeFalse();

        var idIndex = entityType.FindIndex(entityType.FindProperty(nameof(Recipe.Id))!);
        idIndex.Should().NotBeNull();
        idIndex!.IsUnique.Should().BeTrue();

        var nameIndex = entityType.FindIndex(entityType.FindProperty(nameof(Recipe.Name))!);
        nameIndex.Should().NotBeNull();
        nameIndex!.IsUnique.Should().BeTrue();
    }
}
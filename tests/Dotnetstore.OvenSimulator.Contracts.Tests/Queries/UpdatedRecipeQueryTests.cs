using Dotnetstore.OvenSimulator.Contracts.Entities;
using Dotnetstore.OvenSimulator.Contracts.Queries;
using Dotnetstore.OvenSimulator.SDK.Recipes.Requests;
using FluentAssertions;
using MediatR;
using Xunit;

namespace Dotnetstore.OvenSimulator.Contracts.Tests.Queries;

public class UpdatedRecipeQueryTests
{
    [Fact]
    public void UpdatedRecipeQuery_ShouldBeOfTypeIRequest()
    {
        // Arrange
        var request = new UpdateRecipeRequest(Guid.NewGuid(), "test", 0, 0, 0, 0);
        
        // Act
        var query = new UpdatedRecipeQuery(request, null!);

        // Assert
        query.Should().BeAssignableTo<IRequest>();
    }
    
    [Fact]
    public void UpdatedRecipeQuery_ShouldHaveUpdateRecipeRequest()
    {
        // Arrange
        var request = new UpdateRecipeRequest(Guid.NewGuid(), "test", 0, 0, 0, 0);
        
        // Act
        var query = new UpdatedRecipeQuery(request, null!);

        // Assert
        query.UpdateRecipeRequest.Should().Be(request);
    }
    
    [Fact]
    public void UpdatedRecipeQuery_ShouldHaveOldRecipe()
    {
        // Arrange
        var request = new UpdateRecipeRequest(Guid.NewGuid(), "test", 0, 0, 0, 0);
        var oldRecipe = new Recipe
        {
            Id = new RecipeId(Guid.NewGuid()),
            Name = "test",
            HeatCapacity = 0,
            HeaterPowerPercentage = 0,
            HeatLossCoefficient = 0,
            TargetTemperature = 0
        };
        
        // Act
        var query = new UpdatedRecipeQuery(request, oldRecipe);

        // Assert
        query.OldRecipe.Should().Be(oldRecipe);
    }
}
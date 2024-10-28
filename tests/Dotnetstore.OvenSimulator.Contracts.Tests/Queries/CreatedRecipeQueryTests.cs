using Dotnetstore.OvenSimulator.Contracts.Queries;
using Dotnetstore.OvenSimulator.SDK.Recipes.Requests;
using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;
using FluentAssertions;
using MediatR;
using Xunit;

namespace Dotnetstore.OvenSimulator.Contracts.Tests.Queries;

public class CreatedRecipeQueryTests
{
    [Fact]
    public void CreatedRecipeQuery_ShouldBeOfTypeIRequest()
    {
        // Arrange
        var createRecipeRequest = new CreateRecipeRequest();
        var recipeResponse = new RecipeResponse();

        // Act
        var createdRecipeQuery = new CreatedRecipeQuery(createRecipeRequest, recipeResponse);

        // Assert
        createdRecipeQuery.Should().BeAssignableTo<IRequest>();
    }
    
    [Fact]
    public void CreatedRecipeQuery_ShouldHaveCreateRecipeRequest()
    {
        // Arrange
        var createRecipeRequest = new CreateRecipeRequest();
        var recipeResponse = new RecipeResponse();

        // Act
        var createdRecipeQuery = new CreatedRecipeQuery(createRecipeRequest, recipeResponse);

        // Assert
        createdRecipeQuery.CreateRecipeRequest.Should().Be(createRecipeRequest);
    }
    
    [Fact]
    public void CreatedRecipeQuery_ShouldHaveRecipeResponse()
    {
        // Arrange
        var createRecipeRequest = new CreateRecipeRequest();
        var recipeResponse = new RecipeResponse();

        // Act
        var createdRecipeQuery = new CreatedRecipeQuery(createRecipeRequest, recipeResponse);

        // Assert
        createdRecipeQuery.RecipeResponse.Should().Be(recipeResponse);
    }
}
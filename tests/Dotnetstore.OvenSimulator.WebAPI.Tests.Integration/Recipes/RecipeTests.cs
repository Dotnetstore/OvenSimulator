using Dotnetstore.OvenSimulator.Recipes.Create;
using Dotnetstore.OvenSimulator.Recipes.GetAll;
using Dotnetstore.OvenSimulator.SDK;
using Dotnetstore.OvenSimulator.SDK.Recipes.Requests;
using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;
using Dotnetstore.OvenSimulator.SharedKernel.Services;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Dotnetstore.OvenSimulator.WebAPI.Tests.Integration.Recipes;

public class RecipeTests(DotnetstoreOvenSimulatorBase simulatorBase) : TestBase<DotnetstoreOvenSimulatorBase>
{
    [Fact]
    public async Task GetAllRecipes_ShouldReturn2Objects()
    {
        //Act
        var (rsp, res) = await simulatorBase.Client.GETAsync<GetAllRecipeEndpoint, IEnumerable<RecipeResponse>>();
        
        //Assert
        using (new AssertionScope())
        {
            rsp.Should().BeSuccessful();
            res.Should().HaveCount(1);
        }
    }

    [Fact]
    public async Task GetRecipeById_ShouldReturnRecipe()
    {
        // Arrange
        var url = ApiEndpoints.Recipe.GetById.Replace("{id}", DataSchemeConstants.DefaultRecipeIdValue);
        
        // Act
        var response = await simulatorBase.Client.GetAsync(url);
        
        // Assert
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Should().BeSuccessful();
            response.Content.Should().NotBeNull();
        }
    }

    [Fact]
    public async Task GetRecipeById_SendingWrongId_ShouldReturnFail()
    {
        // Arrange
        var url = ApiEndpoints.Recipe.GetById.Replace("{id}", Guid.NewGuid().ToString());
        
        // Act
        var response = await simulatorBase.Client.GetAsync(url);
        
        // Assert
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Should().HaveError();
            response.IsSuccessStatusCode.Should().BeFalse();
        }
    }

    [Fact]
    public async Task GetRecipeById_SendingBadId_ShouldReturnFail()
    {
        // Arrange
        var url = ApiEndpoints.Recipe.GetById.Replace("{id}", "test");
        
        // Act
        var response = await simulatorBase.Client.GetAsync(url);
        
        // Assert
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Should().HaveError();
            response.IsSuccessStatusCode.Should().BeFalse();
        }
    }

    [Fact]
    public async Task GetRecipeByName_ShouldReturnRecipe()
    {
        // Arrange
        var url = ApiEndpoints.Recipe.GetByName.Replace("{name}", "Pizza");
        
        // Act
        var response = await simulatorBase.Client.GetAsync(url);
        
        // Assert
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Should().BeSuccessful();
            response.Content.Should().NotBeNull();
        }
    }

    [Fact]
    public async Task GetRecipeById_SendingEmptyString_ShouldReturnFail()
    {
        // Arrange
        var url = ApiEndpoints.Recipe.GetByName.Replace("{name}", "");
        
        // Act
        var response = await simulatorBase.Client.GetAsync(url);
        
        // Assert
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Should().HaveError();
            response.IsSuccessStatusCode.Should().BeFalse();
        }
    }
    
    [Fact]
    public async Task CreateRecipe_ShouldReturnRecipe()
    {
        // Arrange
        var request = new CreateRecipeRequest("Test", 500.0, 0.1, 100.0, 300.0);
        
        // Act
        var response = await simulatorBase.Client.POSTAsync<CreateRecipeEndpoint, CreateRecipeRequest, RecipeResponse?>(request);
        
        // Assert
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Result.Should().NotBeNull();
            response.Response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
    
    [Fact]
    public async Task CreateRecipe_EmptyName_ShouldFail()
    {
        // Arrange
        var request = new CreateRecipeRequest("", 500.0, 0.1, 100.0, 300.0);
        
        // Act
        var response = await simulatorBase.Client.POSTAsync<CreateRecipeEndpoint, CreateRecipeRequest, RecipeResponse?>(request);
        
        // Assert
        using (new AssertionScope())
        {
            response.Response.IsSuccessStatusCode.Should().BeFalse();
        }
    }
}
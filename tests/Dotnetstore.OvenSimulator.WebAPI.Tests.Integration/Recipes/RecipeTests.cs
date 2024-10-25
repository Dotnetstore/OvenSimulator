using Dotnetstore.OvenSimulator.Recipes.Create;
using Dotnetstore.OvenSimulator.Recipes.GetAll;
using Dotnetstore.OvenSimulator.Recipes.Update;
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
        var adminClient = simulatorBase.CreateClient(c =>
        {
            c.DefaultRequestHeaders.Authorization = new("Bearer", simulatorBase.ApiKey);
        });
        var (rsp, res) = await adminClient.GETAsync<GetAllRecipeEndpoint, IEnumerable<RecipeResponse>>();
        
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
        var adminClient = simulatorBase.CreateClient(c =>
        {
            c.DefaultRequestHeaders.Authorization = new("Bearer", simulatorBase.ApiKey);
        });
        var url = ApiEndpoints.Recipe.GetById.Replace("{id}", DataSchemeConstants.DefaultRecipeIdValue);
        
        // Act
        var response = await adminClient.GetAsync(url);
        
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
        var adminClient = simulatorBase.CreateClient(c =>
        {
            c.DefaultRequestHeaders.Authorization = new("Bearer", simulatorBase.ApiKey);
        });
        var url = ApiEndpoints.Recipe.GetById.Replace("{id}", Guid.NewGuid().ToString());
        
        // Act
        var response = await adminClient.GetAsync(url);
        
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
        var adminClient = simulatorBase.CreateClient(c =>
        {
            c.DefaultRequestHeaders.Authorization = new("Bearer", simulatorBase.ApiKey);
        });
        var url = ApiEndpoints.Recipe.GetById.Replace("{id}", "test");
        
        // Act
        var response = await adminClient.GetAsync(url);
        
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
        var adminClient = simulatorBase.CreateClient(c =>
        {
            c.DefaultRequestHeaders.Authorization = new("Bearer", simulatorBase.ApiKey);
        });
        var url = ApiEndpoints.Recipe.GetByName.Replace("{name}", "Pizza");
        
        // Act
        var response = await adminClient.GetAsync(url);
        
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
        var adminClient = simulatorBase.CreateClient(c =>
        {
            c.DefaultRequestHeaders.Authorization = new("Bearer", simulatorBase.ApiKey);
        });
        var url = ApiEndpoints.Recipe.GetByName.Replace("{name}", "");
        
        // Act
        var response = await adminClient.GetAsync(url);
        
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
        var adminClient = simulatorBase.CreateClient(c =>
        {
            c.DefaultRequestHeaders.Authorization = new("Bearer", simulatorBase.ApiKey);
        });
        var request = new CreateRecipeRequest("Test", 500.0, 0.1, 100.0, 300.0);
        
        // Act
        var response = await adminClient.POSTAsync<CreateRecipeEndpoint, CreateRecipeRequest, RecipeResponse?>(request);
        
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
        var adminClient = simulatorBase.CreateClient(c =>
        {
            c.DefaultRequestHeaders.Authorization = new("Bearer", simulatorBase.ApiKey);
        });
        var request = new CreateRecipeRequest("", 500.0, 0.1, 100.0, 300.0);
        
        // Act
        var response = await adminClient.POSTAsync<CreateRecipeEndpoint, CreateRecipeRequest, RecipeResponse?>(request);
        
        // Assert
        using (new AssertionScope())
        {
            response.Response.IsSuccessStatusCode.Should().BeFalse();
        }
    }
    
    [Fact]
    public async Task UpdateRecipe_ShouldReturnOk()
    {
        // Arrange
        var adminClient = simulatorBase.CreateClient(c =>
        {
            c.DefaultRequestHeaders.Authorization = new("Bearer", simulatorBase.ApiKey);
        });
        var request = new UpdateRecipeRequest(Guid.Parse(DataSchemeConstants.DefaultRecipeIdValue), "Testar", 500.0, 0.1, 100.0, 300.0);
        
        // Act
        var response = await adminClient.PUTAsync<UpdateRecipeEndpoint, UpdateRecipeRequest>(request);
        
        // Assert
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
    
    [Fact]
    public async Task UpdateRecipe_WrongId_ShouldReturnFail()
    {
        // Arrange
        var adminClient = simulatorBase.CreateClient(c =>
        {
            c.DefaultRequestHeaders.Authorization = new("Bearer", simulatorBase.ApiKey);
        });
        var request = new UpdateRecipeRequest(Guid.NewGuid(), "Testar", 500.0, 0.1, 100.0, 300.0);
        
        // Act
        var response = await adminClient.PUTAsync<UpdateRecipeEndpoint, UpdateRecipeRequest>(request);
        
        // Assert
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.IsSuccessStatusCode.Should().BeFalse();
        }
    }
    
    [Fact]
    public async Task DeleteRecipe_ShouldReturnOk()
    {
        // Arrange
        var adminClient = simulatorBase.CreateClient(c =>
        {
            c.DefaultRequestHeaders.Authorization = new("Bearer", simulatorBase.ApiKey);
        });
        var url = ApiEndpoints.Recipe.Delete.Replace("{id}", DataSchemeConstants.DefaultRecipeIdValue);
        
        // Act
        var response = await adminClient.DeleteAsync(url);
        
        // Assert
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.IsSuccessStatusCode.Should().BeTrue();
        }
    }
    
    [Fact]
    public async Task DeleteRecipe_WithWrongId_ShouldReturnFail()
    {
        // Arrange
        var adminClient = simulatorBase.CreateClient(c =>
        {
            c.DefaultRequestHeaders.Authorization = new("Bearer", simulatorBase.ApiKey);
        });
        var url = ApiEndpoints.Recipe.Delete.Replace("{id}", Guid.NewGuid().ToString());
        
        // Act
        var response = await adminClient.DeleteAsync(url);
        
        // Assert
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.IsSuccessStatusCode.Should().BeFalse();
        }
    }
}
using FluentAssertions;

namespace Dotnetstore.OvenSimulator.SDK.Tests;

using Xunit;

public class ApiEndpointsTests
{
    [Fact]
    public void Oven_LoadRecipe_ShouldReturnCorrectUrl()
    {
        // Act
        const string url = ApiEndpoints.Oven.LoadRecipe;

        // Assert
        url.Should().Be("/api/ovens/load-recipe");
    }

    [Fact]
    public void Oven_Start_ShouldReturnCorrectUrl()
    {
        // Act
        const string url = ApiEndpoints.Oven.Start;

        // Assert
        url.Should().Be("/api/ovens/start");
    }

    [Fact]
    public void Oven_Stop_ShouldReturnCorrectUrl()
    {
        // Act
        const string url = ApiEndpoints.Oven.Stop;

        // Assert
        url.Should().Be("/api/ovens/stop");
    }

    [Fact]
    public void Oven_Get_ShouldReturnCorrectUrl()
    {
        // Act
        const string url = ApiEndpoints.Oven.Get;

        // Assert
        url.Should().Be("/api/ovens");
    }

    [Fact]
    public void Oven_AddError_ShouldReturnCorrectUrl()
    {
        // Act
        const string url = ApiEndpoints.Oven.AddError;

        // Assert
        url.Should().Be("/api/ovens/add-error");
    }

    [Fact]
    public void Recipe_GetAll_ShouldReturnCorrectUrl()
    {
        // Act
        const string url = ApiEndpoints.Recipe.GetAll;

        // Assert
        url.Should().Be("/api/recipes");
    }

    [Fact]
    public void Recipe_GetById_ShouldReturnCorrectUrl()
    {
        // Act
        const string url = ApiEndpoints.Recipe.GetById;

        // Assert
        url.Should().Be("/api/recipes/getById/{id}");
    }

    [Fact]
    public void Recipe_GetByName_ShouldReturnCorrectUrl()
    {
        // Act
        const string url = ApiEndpoints.Recipe.GetByName;

        // Assert
        url.Should().Be("/api/recipes/getByName/{name}");
    }

    [Fact]
    public void Recipe_Create_ShouldReturnCorrectUrl()
    {
        // Act
        const string url = ApiEndpoints.Recipe.Create;

        // Assert
        url.Should().Be("/api/recipes");
    }

    [Fact]
    public void Recipe_Update_ShouldReturnCorrectUrl()
    {
        // Act
        const string url = ApiEndpoints.Recipe.Update;

        // Assert
        url.Should().Be("/api/recipes");
    }

    [Fact]
    public void Recipe_Delete_ShouldReturnCorrectUrl()
    {
        // Act
        const string url = ApiEndpoints.Recipe.Delete;

        // Assert
        url.Should().Be("/api/recipes/delete/{id}");
    }
}
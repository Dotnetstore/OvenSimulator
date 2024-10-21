using Dotnetstore.OvenSimulator.SDK.Recipes.Requests;
using FluentAssertions;
using Xunit;

namespace Dotnetstore.OvenSimulator.SDK.Tests.Recipes.Requests;

public class GetRecipeByIdRequestTests
{
    [Fact]
    public void GetRecipeByIdRequest_ShouldInitializePropertiesCorrectly()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var getRecipeByIdRequest = new GetRecipeByIdRequest(id);

        // Assert
        getRecipeByIdRequest.Id.Should().Be(id);
    }
}
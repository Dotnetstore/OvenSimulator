using Dotnetstore.OvenSimulator.SDK.Oven.Responses;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Dotnetstore.OvenSimulator.SDK.Tests.Oven.Responses;

public class GetOvenStatusResponseTests
{
    [Fact]
    public void GetOvenStatusResponse_ShouldHaveProperties()
    {
        // Act
        var response = new GetOvenStatusResponse(0, false, "recipe", "error");

        // Assert
        using (new AssertionScope())
        {
            response.CurrentTemperature.Should().Be(0);
            response.HeatingElementOn.Should().BeFalse();
            response.ActiveRecipe.Should().Be("recipe");
            response.CurrentError.Should().Be("error");
        }
    }
}
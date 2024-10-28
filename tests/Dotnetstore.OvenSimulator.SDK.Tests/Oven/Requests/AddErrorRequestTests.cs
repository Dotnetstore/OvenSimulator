using Dotnetstore.OvenSimulator.SDK.Oven;
using Dotnetstore.OvenSimulator.SDK.Oven.Requests;
using FluentAssertions;
using Xunit;

namespace Dotnetstore.OvenSimulator.SDK.Tests.Oven.Requests;

public class AddErrorRequestTests
{
    [Fact]
    public void AddErrorRequest_ShouldHaveErrorType()
    {
        // Act
        var request = new AddErrorRequest(OvenErrorType.None);

        // Assert
        request.ErrorType.Should().Be(OvenErrorType.None);
    }
}
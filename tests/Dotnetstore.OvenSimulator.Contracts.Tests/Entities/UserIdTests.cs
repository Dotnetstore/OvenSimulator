using Dotnetstore.OvenSimulator.Contracts.Entities;
using FluentAssertions;
using Xunit;

namespace Dotnetstore.OvenSimulator.Contracts.Tests.Entities;

public class UserIdTests
{
    [Fact]
    public void UserId_ShouldHaveCorrectId()
    {
        // Arrange
        var id = Guid.NewGuid();

        // Act
        var userId = new UserId(id);

        // Assert
        userId.Value.Should().Be(id);
    }
}
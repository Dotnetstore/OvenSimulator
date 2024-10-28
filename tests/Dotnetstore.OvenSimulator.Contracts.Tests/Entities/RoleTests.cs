using Dotnetstore.OvenSimulator.Contracts.Entities;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Dotnetstore.OvenSimulator.Contracts.Tests.Entities;

public class RoleTests
{
    [Fact]
    public void Role_ShouldContainCorrectProperties()
    {
        // Arrange
        const string name = "Admin";
        var userId = new UserId();
        
        // Act
        var role = new Role
        {
            Name = name,
            UserId = userId
        };

        // Assert
        using (new AssertionScope())
        {
            role.Name.Should().Be(name);
            role.UserId.Should().Be(userId);
        }
    }
}
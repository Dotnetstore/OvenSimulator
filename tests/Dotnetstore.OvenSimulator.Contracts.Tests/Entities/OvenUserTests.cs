using Dotnetstore.OvenSimulator.Contracts.Entities;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Dotnetstore.OvenSimulator.Contracts.Tests.Entities;

public class OvenUserTests
{
    [Fact]
    public void OvenUser_ShouldContainCorrectProperties()
    {
        // Arrange
        var id = new UserId(Guid.NewGuid());
        const string name = "John Doe";
        const string username = "johndoe";
        const string password = "password";
        
        // Act
        var ovenUser = new OvenUser
        {
            // Act
            Id = id,
            Name = name,
            Username = username,
            Password = password,
            Roles = new List<Role>()
        };

        // Assert
        using (new AssertionScope())
        {
            ovenUser.Id.Should().Be(id);
            ovenUser.Name.Should().Be(name);
            ovenUser.Username.Should().Be(username);
            ovenUser.Password.Should().Be(password);
            ovenUser.Roles.Should().BeEmpty();
        }
    }
}
using Dotnetstore.OvenSimulator.SDK.Users.Requests;
using FluentAssertions;
using Xunit;

namespace Dotnetstore.OvenSimulator.SDK.Tests.Users.Requests;

public class LoginRequestTests
{
    [Fact]
    public void LoginRequest_ShouldHaveUsername()
    {
        // Act
        var request = new LoginRequest("username", "password");

        // Assert
        request.Username.Should().Be("username");
    }
    
    [Fact]
    public void LoginRequest_ShouldHavePassword()
    {
        // Act
        var request = new LoginRequest("username", "password");

        // Assert
        request.Password.Should().Be("password");
    }
}
using Dotnetstore.OvenSimulator.SDK.Users.Requests;
using Dotnetstore.OvenSimulator.Users;
using FastEndpoints;
using FastEndpoints.Testing;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Dotnetstore.OvenSimulator.WebAPI.Tests.Integration.Users;

public class UsersTests(DotnetstoreOvenSimulatorBase simulatorBase) : TestBase<DotnetstoreOvenSimulatorBase>
{
    [Fact]
    public async Task Login_ShouldReturnToken()
    {
        // Arrange
        var loginRequest = new LoginRequest { Username = "test@test.com", Password = "test" };
        
        // Act
        var response = await simulatorBase.Client.POSTAsync<UserLoginEndpoint, LoginRequest>(loginRequest);
        
        // Assert
        using (new AssertionScope())
        {
            response.Should().NotBeNull();
            response.Should().BeSuccessful();
            response.Content.Should().NotBeNull();
        }
    }
}
using Dotnetstore.OvenSimulator.Contracts.Queries;
using FluentAssertions;
using MediatR;
using Xunit;

namespace Dotnetstore.OvenSimulator.Contracts.Tests.Queries;

public class SendOvenErrorEmailQueryTests
{
    [Fact]
    public void SendOvenErrorEmailQuery_ShouldBeOfTypeIRequest()
    {
        // Act
        var query = new SendOvenErrorEmailQuery("error message");

        // Assert
        query.Should().BeAssignableTo<IRequest>();
    }
    
    [Fact]
    public void SendOvenErrorEmailQuery_ShouldHaveErrorMessage()
    {
        // Act
        var query = new SendOvenErrorEmailQuery("error message");

        // Assert
        query.ErrorMessage.Should().Be("error message");
    }
}
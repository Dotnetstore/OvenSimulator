using Dotnetstore.OvenSimulator.Contracts.Queries;
using FluentAssertions;
using MediatR;
using Xunit;

namespace Dotnetstore.OvenSimulator.Contracts.Tests.Queries;

public class DeletedRecipeQueryTests
{
    [Fact]
    public void DeletedRecipeQuery_ShouldBeOfTypeIRequest()
    {
        // Act
        var query = new DeletedRecipeQuery(Guid.NewGuid());

        // Assert
        query.Should().BeAssignableTo<IRequest>();
    }
}
using Dotnetstore.OvenSimulator.Contracts.Commands;
using FluentAssertions;
using Xunit;

namespace Dotnetstore.OvenSimulator.Contracts.Tests.Commands;

public class GetRecipeByNameCommandTests
{
    [Fact]
    public void GetRecipeByNameCommand_ShouldHaveName()
    {
        // Arrange
        const string name = "TestName";

        // Act
        var command = new GetRecipeByNameCommand(name);

        // Assert
        command.Name.Should().Be(name);
    }
}
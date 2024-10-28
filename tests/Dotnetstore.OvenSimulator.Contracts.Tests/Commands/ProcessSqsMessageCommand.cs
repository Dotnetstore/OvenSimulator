using FluentAssertions;
using Xunit;

namespace Dotnetstore.OvenSimulator.Contracts.Tests.Commands;

public class ProcessSqsMessageCommand
{
    [Fact]
    public void ProcessSqsMessageCommand_ShouldExist()
    {
        // Act
        var command = new ProcessSqsMessageCommand();

        // Assert
        command.Should().NotBeNull();
    }
}
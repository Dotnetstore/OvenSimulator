using Dotnetstore.OvenSimulator.Amazon.Commands;
using Dotnetstore.OvenSimulator.Amazon.Services;
using Dotnetstore.OvenSimulator.Contracts.Commands;
using MediatR;
using NSubstitute;
using Xunit;

namespace Dotnetstore.OvenSimulator.Amazon.Tests.Commands;

public class ProcessSqsMessageCommandHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCallReceiveMessageAsync()
    {
        // Arrange
        var amazonSqsService = Substitute.For<IAmazonSqsService>();
        var handler = new ProcessSqsMessageCommandHandler(amazonSqsService);
        var command = new ProcessSqsMessageCommand();
        var cancellationToken = CancellationToken.None;

        // Act
        await ((IRequestHandler<ProcessSqsMessageCommand>)handler).Handle(command, cancellationToken);

        // Assert
        await amazonSqsService.Received(1).ReceiveMessageAsync(cancellationToken);
    }
}
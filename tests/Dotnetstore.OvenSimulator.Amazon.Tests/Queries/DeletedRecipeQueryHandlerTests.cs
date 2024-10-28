using Dotnetstore.OvenSimulator.Amazon.Queries;
using Dotnetstore.OvenSimulator.Amazon.Services;
using Dotnetstore.OvenSimulator.Contracts.Queries;
using NSubstitute;
using Xunit;
using MediatR;

namespace Dotnetstore.OvenSimulator.Amazon.Tests.Queries;

public class DeletedRecipeQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCallSendMessageAsync()
    {
        // Arrange
        var amazonSqsService = Substitute.For<IAmazonSqsService>();
        var handler = new DeletedRecipeQueryHandler(amazonSqsService);
        var query = new DeletedRecipeQuery(Guid.NewGuid());
        var cancellationToken = CancellationToken.None;

        // Act
        await ((IRequestHandler<DeletedRecipeQuery>)handler).Handle(query, cancellationToken);

        // Assert
        await amazonSqsService.Received(1).SendMessageAsync(query, cancellationToken);
    }
}
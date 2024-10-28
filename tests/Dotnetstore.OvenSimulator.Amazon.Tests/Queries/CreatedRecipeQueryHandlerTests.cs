using Dotnetstore.OvenSimulator.Amazon.Queries;
using Dotnetstore.OvenSimulator.Amazon.Services;
using Dotnetstore.OvenSimulator.Contracts.Queries;
using NSubstitute;
using Dotnetstore.OvenSimulator.SDK.Recipes.Requests;
using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;
using MediatR;
using Xunit;

namespace Dotnetstore.OvenSimulator.Amazon.Tests.Queries;

public class CreatedRecipeQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCallSendMessageAsync()
    {
        // Arrange
        var amazonSqsService = Substitute.For<IAmazonSqsService>();
        var handler = new CreatedRecipeQueryHandler(amazonSqsService);
        var query = new CreatedRecipeQuery(
            new CreateRecipeRequest("Test Recipe", 500.0, 0.5, 50.0, 200.0),
            new RecipeResponse(Guid.NewGuid(), "Test Recipe", 500.0, 0.5, 50.0, 200.0)
        );
        var cancellationToken = CancellationToken.None;

        // Act
        // await handler.Handle(query, cancellationToken);
        await ((IRequestHandler<CreatedRecipeQuery>)handler).Handle(query, cancellationToken);
        
        // Assert
        await amazonSqsService.Received(1).SendMessageAsync(query, cancellationToken);
    }
}
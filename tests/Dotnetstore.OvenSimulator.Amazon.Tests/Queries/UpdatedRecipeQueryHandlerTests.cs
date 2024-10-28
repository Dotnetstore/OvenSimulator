using Dotnetstore.OvenSimulator.Amazon.Queries;
using Dotnetstore.OvenSimulator.Amazon.Services;
using Dotnetstore.OvenSimulator.Contracts.Queries;
using Dotnetstore.OvenSimulator.SDK.Recipes.Requests;
using Dotnetstore.OvenSimulator.Contracts.Entities;
using NSubstitute;
using Xunit;
using MediatR;

namespace Dotnetstore.OvenSimulator.Amazon.Tests.Queries;

public class UpdatedRecipeQueryHandlerTests
{
    [Fact]
    public async Task Handle_ShouldCallSendMessageAsync()
    {
        // Arrange
        var amazonSqsService = Substitute.For<IAmazonSqsService>();
        var handler = new UpdatedRecipeQueryHandler(amazonSqsService);
        var query = new UpdatedRecipeQuery(
            new UpdateRecipeRequest(Guid.NewGuid(), "Updated Recipe", 500.0, 0.5, 50.0, 200.0),
            new Recipe { Id = new RecipeId(Guid.NewGuid()), Name = "Old Recipe", HeatCapacity = 400.0, HeatLossCoefficient = 0.4, HeaterPowerPercentage = 40.0, TargetTemperature = 180.0 }
        );
        var cancellationToken = CancellationToken.None;

        // Act
        await ((IRequestHandler<UpdatedRecipeQuery>)handler).Handle(query, cancellationToken);

        // Assert
        await amazonSqsService.Received(1).SendMessageAsync(query, cancellationToken);
    }
}
using Dotnetstore.OvenSimulator.Amazon.Services;
using Dotnetstore.OvenSimulator.Contracts.Queries;
using MediatR;

namespace Dotnetstore.OvenSimulator.Amazon.Queries;

internal sealed class CreatedRecipeQueryHandler(IAmazonSqsService amazonSqsService) : IRequestHandler<CreatedRecipeQuery>
{
    async Task IRequestHandler<CreatedRecipeQuery>.
        Handle(CreatedRecipeQuery request, CancellationToken cancellationToken)
    {
        await amazonSqsService.SendMessageAsync(request, cancellationToken);
    }
}
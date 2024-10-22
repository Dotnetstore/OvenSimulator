using Dotnetstore.OvenSimulator.Amazon.Services;
using Dotnetstore.OvenSimulator.Contracts.Queries;
using MediatR;

namespace Dotnetstore.OvenSimulator.Amazon.Queries;

internal sealed class UpdatedRecipeQueryHandler(IAmazonSqsService amazonSqsService) : IRequestHandler<UpdatedRecipeQuery>
{
    async Task IRequestHandler<UpdatedRecipeQuery>
        .Handle(UpdatedRecipeQuery request, CancellationToken cancellationToken)
    {
        await amazonSqsService.SendMessageAsync(request, cancellationToken);
    }
}
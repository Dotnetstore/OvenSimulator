using Dotnetstore.OvenSimulator.Amazon.Services;
using Dotnetstore.OvenSimulator.Contracts.Queries;
using MediatR;

namespace Dotnetstore.OvenSimulator.Amazon.Queries;

internal sealed class DeletedRecipeQueryHandler(IAmazonSqsService amazonSqsService) : IRequestHandler<DeletedRecipeQuery>
{
    async Task IRequestHandler<DeletedRecipeQuery>
        .Handle(DeletedRecipeQuery request, CancellationToken cancellationToken)
    {
        await amazonSqsService.SendMessageAsync(request, cancellationToken);
    }
}
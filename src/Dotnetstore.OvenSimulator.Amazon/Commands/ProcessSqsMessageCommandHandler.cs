using Dotnetstore.OvenSimulator.Amazon.Services;
using Dotnetstore.OvenSimulator.Contracts.Commands;
using MediatR;

namespace Dotnetstore.OvenSimulator.Amazon.Commands;

internal sealed class ProcessSqsMessageCommandHandler(IAmazonSqsService amazonSqsService) : IRequestHandler<ProcessSqsMessageCommand>
{
    async Task IRequestHandler<ProcessSqsMessageCommand>.Handle(ProcessSqsMessageCommand request, CancellationToken cancellationToken)
    {
        await amazonSqsService.ReceiveMessageAsync(cancellationToken);
    }
}
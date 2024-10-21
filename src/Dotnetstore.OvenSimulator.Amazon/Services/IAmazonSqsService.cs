using Amazon.SQS.Model;

namespace Dotnetstore.OvenSimulator.Amazon.Services;

public interface IAmazonSqsService
{
    ValueTask<string> CreateQueueWithName(string queueName, bool useFifoQueue);

    ValueTask<SendMessageResponse> SendMessageAsync<T>(T message, CancellationToken cancellationToken = default);
    
    ValueTask ReceiveMessageAsync(CancellationToken cancellationToken = default);
}
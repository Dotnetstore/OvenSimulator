using System.Text.Json;
using Amazon.SQS;
using Amazon.SQS.Model;
using Dotnetstore.OvenSimulator.Contracts.Queries;
using Dotnetstore.OvenSimulator.SharedKernel.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Dotnetstore.OvenSimulator.Amazon.Services;

internal sealed class AmazonSqsService(
    IOptions<AmazonSettings> options,
    ILogger<AmazonSqsService> logger) : IAmazonSqsService
{
    private readonly AmazonSQSClient _amazonSqsClient = new(options.Value.DefaultRegion);
    
    async ValueTask<string> IAmazonSqsService.CreateQueueWithName(string queueName, bool useFifoQueue)
    {
        const int maxMessage = 256 * 1024;
        var queueAttributes = new Dictionary<string, string>
        {
            {
                QueueAttributeName.MaximumMessageSize,
                maxMessage.ToString()
            }
        };

        var createQueueRequest = new CreateQueueRequest
        {
            QueueName = queueName,
            Attributes = queueAttributes
        };

        if (useFifoQueue)
        {
            if (!queueName.EndsWith(".fifo"))
            {
                createQueueRequest.QueueName = $"{queueName}.fifo";
            }

            createQueueRequest.Attributes.Add(QueueAttributeName.FifoQueue, "true");
        }

        var createResponse = await _amazonSqsClient.CreateQueueAsync(
            new CreateQueueRequest
            {
                QueueName = queueName
            });
        return createResponse.QueueUrl;
    }
    
    async ValueTask<SendMessageResponse> IAmazonSqsService.SendMessageAsync<T>(T message, CancellationToken cancellationToken)
    {
        var queueUrlResponse = await _amazonSqsClient.GetQueueUrlAsync(options.Value.SqsQueueName, cancellationToken);
        
        var sendMessageRequest = new SendMessageRequest
        {
            QueueUrl = queueUrlResponse.QueueUrl,
            MessageBody = JsonSerializer.Serialize(message),
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                {
                    "MessageType", new MessageAttributeValue
                    {
                        DataType = "String",
                        StringValue = typeof(T).Name
                    }
                }
            }
        };
        
        var response = await _amazonSqsClient.SendMessageAsync(sendMessageRequest, cancellationToken);

        return response;
    }

    async ValueTask IAmazonSqsService.ReceiveMessageAsync(CancellationToken cancellationToken)
    {
        var queueUrlResponse = await _amazonSqsClient.GetQueueUrlAsync(options.Value.SqsQueueName, cancellationToken);

        var receiveMessageRequest = new ReceiveMessageRequest
        {
            QueueUrl = queueUrlResponse.QueueUrl,
            MessageAttributeNames = ["All"]
        };

        while (!cancellationToken.IsCancellationRequested)
        {
            var response = await _amazonSqsClient.ReceiveMessageAsync(receiveMessageRequest, cancellationToken);

            foreach (var message in response.Messages)
            {
                var messageType = message.MessageAttributes["MessageType"].StringValue;

                switch (messageType)
                {
                    case "CreatedRecipeQuery":
                        var query = JsonSerializer.Deserialize<CreatedRecipeQuery>(message.Body);
                        logger.LogInformation("Recipe received to be taken cared of: {CreatedRecipeQuery}", query.RecipeResponse);
                        break;
                    default:
                        logger.LogInformation("Message type not recognized: {MessageType}", messageType);
                        break;
                }
                
                await _amazonSqsClient.DeleteMessageAsync(queueUrlResponse.QueueUrl, message.ReceiptHandle, cancellationToken);
            }
            
            await Task.Delay(1000, cancellationToken);
        }
    }
}
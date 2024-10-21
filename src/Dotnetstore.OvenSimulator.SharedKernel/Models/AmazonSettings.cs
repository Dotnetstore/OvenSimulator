using Amazon;

namespace Dotnetstore.OvenSimulator.SharedKernel.Models;

public sealed class AmazonSettings
{
    public const string Key = "Amazon";
    
    public readonly RegionEndpoint DefaultRegion = RegionEndpoint.EUNorth1;

    public string SqsQueueName { get; init; } = null!;
}
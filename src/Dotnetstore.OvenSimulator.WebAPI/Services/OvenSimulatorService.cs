using Dotnetstore.OvenSimulator.Contracts.Commands;
using MediatR;

namespace Dotnetstore.OvenSimulator.WebAPI.Services;

internal sealed class OvenSimulatorService(ISender sender) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var tasks = new List<Task> { Task.Run(() => ProcessSqsMessageAsync(stoppingToken), stoppingToken) };

        await Task.WhenAll(tasks);
    }
    
    private async ValueTask ProcessSqsMessageAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var command = new ProcessSqsMessageCommand();
            await sender.Send(command, stoppingToken);
            
            await Task.Delay(5000, stoppingToken);
        }
    }
}
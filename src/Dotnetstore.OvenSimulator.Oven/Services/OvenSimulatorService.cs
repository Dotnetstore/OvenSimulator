using Microsoft.Extensions.Hosting;

namespace Dotnetstore.OvenSimulator.Oven.Services;

internal sealed class OvenSimulatorService(
    IOvenSimulator ovenSimulator) : BackgroundService
{
    private readonly object _lockObject = new();
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var tasks = new List<Task> { Task.Run(() => UpdateOvenAsync(stoppingToken), stoppingToken) };

        await Task.WhenAll(tasks);
    }
    
    private async Task UpdateOvenAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            lock (_lockObject)
            {
                ovenSimulator.Update(1);
            }
            
            await Task.Delay(1000, stoppingToken);
        }
    }
}
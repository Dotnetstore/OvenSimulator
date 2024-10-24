using System.Reflection;
using Dotnetstore.OvenSimulator.Oven.Health;
using Dotnetstore.OvenSimulator.Oven.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetstore.OvenSimulator.Oven.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOvenSimulator(this IServiceCollection services,
        List<Assembly> mediatRAssemblies)
    {
        mediatRAssemblies.Add(typeof(IOvenAssemblyMarker).Assembly);
        
        services
            .AddHostedService<OvenSimulatorService>()
            .AddSingleton<IOvenSimulator, Services.OvenSimulator>()
            .AddScoped<IOvenService, OvenService>();
        
        services
            .AddHealthChecks()
            .AddCheck<OvenHealthCheck>("Oven");
        
        return services;
    }
}
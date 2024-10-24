using System.Reflection;
using Dotnetstore.OvenSimulator.Oven.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetstore.OvenSimulator.Oven.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddOvenSimulator(this IServiceCollection services,
        List<Assembly> mediatRAssemblies)
    {
        mediatRAssemblies.Add(typeof(IOvenAssemblyMarker).Assembly);
        
        services
            .AddSingleton<IOvenSimulator, Services.OvenSimulator>();
        
        return services;
    }
}
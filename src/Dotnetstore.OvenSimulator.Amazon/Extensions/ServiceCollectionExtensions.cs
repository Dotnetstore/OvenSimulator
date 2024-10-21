using System.Reflection;
using Dotnetstore.OvenSimulator.Amazon.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetstore.OvenSimulator.Amazon.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAmazon(this IServiceCollection services, List<Assembly> mediatRAssemblies)
    {
        mediatRAssemblies.Add(typeof(IAmazonAssemblyMarker).Assembly);
        
        services
            .AddSingleton<IAmazonSqsService, AmazonSqsService>();

        return services;
    }
}
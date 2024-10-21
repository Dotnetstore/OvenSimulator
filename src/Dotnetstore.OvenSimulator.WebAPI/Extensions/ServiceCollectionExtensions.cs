using System.Reflection;
using Dotnetstore.OvenSimulator.Amazon.Extensions;
using Dotnetstore.OvenSimulator.Recipes.Extensions;
using Dotnetstore.OvenSimulator.SharedKernel.Behavior;
using Dotnetstore.OvenSimulator.SharedKernel.Extensions;
using Dotnetstore.OvenSimulator.WebAPI.Services;
using FastEndpoints;
using FastEndpoints.Swagger;
using MediatR;
using Serilog;

namespace Dotnetstore.OvenSimulator.WebAPI.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddWebApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        List<Assembly> mediatRAssemblies =
        [
            typeof(Program).Assembly
        ];

        services
            .AddHostedService<OvenSimulatorService>()
            .AddSerilog()
            .AddFastEndpoints()
            .SwaggerDocument()
            .AddSharedKernel(configuration)
            .AddAmazon(mediatRAssemblies)
            //     .AddOven(configuration, mediatRAssemblies)
            .AddRecipes(configuration, mediatRAssemblies)
            .AddMediatR(x => x.RegisterServicesFromAssemblies(mediatRAssemblies.ToArray()))
            .AddHealthChecks();
        
        services
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        
        return services;
    }
}
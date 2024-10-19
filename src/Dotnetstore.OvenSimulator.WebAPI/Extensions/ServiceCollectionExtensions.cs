using Dotnetstore.OvenSimulator.Recipes.Extensions;
using Dotnetstore.OvenSimulator.SharedKernel.Extensions;
using FastEndpoints;
using FastEndpoints.Swagger;
using Serilog;

namespace Dotnetstore.OvenSimulator.WebAPI.Extensions;

internal static class ServiceCollectionExtensions
{
    internal static IServiceCollection AddWebApi(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // List<Assembly> mediatRAssemblies =
        // [
        //     typeof(Program).Assembly,
        //     typeof(IContractsAssemblyMarker).Assembly
        //
        // ];

        services
            //     .AddHostedService<HMISimulatorService>()
            .AddSerilog()
            .AddFastEndpoints()
            .SwaggerDocument()
            .AddSharedKernel(configuration)
            //     .AddAmazon(mediatRAssemblies)
            //     .AddOven(configuration, mediatRAssemblies)
            .AddRecipes(configuration)
            //     .AddMediatR(x => x.RegisterServicesFromAssemblies(mediatRAssemblies.ToArray()))
            .AddHealthChecks();
        
        // services
        //     .AddValidatorsFromAssemblyContaining<IAmazonAssemblyMarker>()
        //     .AddMediatRLoggingBehavior()
        //     .AddMediatRFluentValidationBehavior();
        
        return services;
    }
}
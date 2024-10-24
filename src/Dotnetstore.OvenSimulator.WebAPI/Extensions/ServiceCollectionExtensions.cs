using System.Reflection;
using Dotnetstore.OvenSimulator.Amazon.Extensions;
using Dotnetstore.OvenSimulator.Oven.Extensions;
using Dotnetstore.OvenSimulator.Recipes.Extensions;
using Dotnetstore.OvenSimulator.SharedKernel.Behavior;
using Dotnetstore.OvenSimulator.SharedKernel.Extensions;
using Dotnetstore.OvenSimulator.Users;
using Dotnetstore.OvenSimulator.WebAPI.Services;
using FastEndpoints;
using FastEndpoints.Security;
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
            .AddAuthenticationCookie(validFor: TimeSpan.FromMinutes(10))
            .AddAuthenticationJwtBearer(s => s.SigningKey = "This is a secret key. It should not be spread to anyone. Keep it safe. I keep it here because it is a demo.")
            .AddAuthorization()
            .AddSerilog()
            .AddFastEndpoints()
            .SwaggerDocument()
            .AddSharedKernel(configuration)
            .AddAmazon(mediatRAssemblies)
            //     .AddOven(configuration, mediatRAssemblies)
            .AddRecipes(configuration, mediatRAssemblies)
            .AddOvenSimulator(mediatRAssemblies)
            .AddUsers()
            .AddMediatR(x => x.RegisterServicesFromAssemblies(mediatRAssemblies.ToArray()))
            .AddHealthChecks();
        
        services
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        
        return services;
    }
}
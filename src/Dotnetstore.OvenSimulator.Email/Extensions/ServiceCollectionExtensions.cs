using System.Reflection;
using Dotnetstore.OvenSimulator.Email.Models;
using Dotnetstore.OvenSimulator.Email.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dotnetstore.OvenSimulator.Email.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddEmail(
        this IServiceCollection services,
        IConfiguration configuration,
        List<Assembly> mediatrAssemblies)
    {
        mediatrAssemblies.Add(typeof(IEmailAssemblyMarker).Assembly);

        services
            .AddScoped<IEmailService, EmailService>()
            .Configure<Smtp>(configuration.GetSection(Smtp.Key))
            .AddFluentEmail(configuration.GetValue<string>("Smtp:ErrorFrom"),
                configuration.GetValue<string>("Smtp:ErrorSender"))
            .AddSmtpSender(configuration.GetValue<string>("Smtp:Host"),
                configuration.GetValue<int>("Smtp:Port"));
            
        return services;
    }
}
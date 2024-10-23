using Microsoft.Extensions.DependencyInjection;

namespace Dotnetstore.OvenSimulator.Users;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUsers(this IServiceCollection services)
    {
        services
            .AddScoped<IUserService, UserService>();
        
        return services;
    }
}
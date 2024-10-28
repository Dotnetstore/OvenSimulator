using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace Dotnetstore.OvenSimulator.TestHelper;

public sealed class ServiceCollectionVerifier(IServiceCollection serviceCollection)
{
    public void ContainsSingletonService<TService, TInstance>()
    {
        IsRegistered<TService, TInstance>(ServiceLifetime.Singleton);
    }
    
    public void ContainsTransientService<TService, TInstance>()
    {
        IsRegistered<TService, TInstance>(ServiceLifetime.Transient);
    }
    
    public void ContainsScopedService<TService, TInstance>()
    {
        IsRegistered<TService, TInstance>(ServiceLifetime.Scoped);
    }
    
    private void IsRegistered<TService, TInstance>(ServiceLifetime lifetime)
    {
        serviceCollection.Received().Add(Arg.Is<ServiceDescriptor>(descriptor =>
            descriptor.ServiceType == typeof(TService) &&
            descriptor.ImplementationType == typeof(TInstance) &&
            descriptor.Lifetime == lifetime));

    }
}
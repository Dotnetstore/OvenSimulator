using Microsoft.Extensions.DependencyInjection;
using NSubstitute;

namespace Dotnetstore.OvenSimulator.TestHelper;

public sealed class ServiceCollectionMock
{
    public ServiceCollectionVerifier ServiceCollectionVerifier { get; set; }

    public IServiceCollection ServiceCollection { get; set; } = Substitute.For<IServiceCollection>();
    
    public ServiceCollectionMock()
    {
        ServiceCollectionVerifier = new ServiceCollectionVerifier(ServiceCollection);
    }
    
    public void ContainsSingletonService<TService, TInstance>()
    {
        ServiceCollectionVerifier.ContainsSingletonService<TService, TInstance>();
    }
    
    public void ContainsTransientService<TService, TInstance>()
    {
        ServiceCollectionVerifier.ContainsTransientService<TService, TInstance>();
    }
    
    public void ContainsScopedService<TService, TInstance>()
    {
        ServiceCollectionVerifier.ContainsTransientService<TService, TInstance>();
    }
}
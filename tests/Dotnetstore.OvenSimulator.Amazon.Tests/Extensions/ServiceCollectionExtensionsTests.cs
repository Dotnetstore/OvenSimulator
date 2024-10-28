using System.Reflection;
using Dotnetstore.OvenSimulator.Amazon.Extensions;
using Dotnetstore.OvenSimulator.Amazon.Services;
using Dotnetstore.OvenSimulator.TestHelper;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;

namespace Dotnetstore.OvenSimulator.Amazon.Tests.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddAmazon_ShouldRegisterCorrectServices()
    {
        // Arrange
        var mediatrAssemblies = new List<Assembly>();
        var serviceCollection = new ServiceCollectionMock();
        
        // Act
        serviceCollection.ServiceCollection.AddAmazon(mediatrAssemblies);
        
        // Assert
        using (new AssertionScope())
        {
            serviceCollection.ContainsSingletonService<IAmazonSqsService, AmazonSqsService>();
            mediatrAssemblies.Should().Contain(typeof(IAmazonAssemblyMarker).Assembly);
        }
    }
}
using Dotnetstore.OvenSimulator.SharedKernel.Abstracts;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;

namespace Dotnetstore.OvenSimulator.SharedKernel.Tests.Abstracts;

public class ApiContextTests
{
    [Fact]
    public void OnModelCreating_ShouldApplyConfigurationsFromAssembly()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        var modelBuilder = Substitute.For<ModelBuilder>();
        var context = new TestDbContext(options);

        // Act
        context.GetType().BaseType!
            .GetMethod("OnModelCreating", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)!
            .Invoke(context, new object[] { modelBuilder });

        // Assert
        modelBuilder.Received().ApplyConfigurationsFromAssembly(typeof(ISharedKernelAssemblyMarker).Assembly);
    }
}

public class TestDbContext(DbContextOptions<TestDbContext> options) : ApiContext<TestDbContext>(options);
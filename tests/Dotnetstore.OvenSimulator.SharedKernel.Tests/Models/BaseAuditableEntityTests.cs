using Dotnetstore.OvenSimulator.SharedKernel.Models;
using FluentAssertions;
using Xunit;

namespace Dotnetstore.OvenSimulator.SharedKernel.Tests.Models;

public class BaseAuditableEntityTests
{
    private class TestAuditableEntity : BaseAuditableEntity
    {
        public Guid Id { get; init; }
    }

    [Fact]
    public void TestAuditableEntity_ShouldBeAssignableToIBaseAuditableEntity()
    {
        // Arrange
        var entity = new TestAuditableEntity();

        // Act & Assert
        entity.Should().BeAssignableTo<IBaseAuditableEntity>();
    }

    [Fact]
    public void TestAuditableEntity_ShouldHaveIdProperty()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new TestAuditableEntity { Id = id };

        // Act & Assert
        entity.Id.Should().Be(id);
    }
}
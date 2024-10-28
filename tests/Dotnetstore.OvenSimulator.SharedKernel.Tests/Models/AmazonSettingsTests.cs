using Amazon;
using Dotnetstore.OvenSimulator.SharedKernel.Models;
using FluentAssertions;
using Xunit;

namespace Dotnetstore.OvenSimulator.SharedKernel.Tests.Models
{
    public class AmazonSettingsTests
    {
        [Fact]
        public void AmazonSettings_ShouldHaveCorrectDefaultValues()
        {
            // Arrange & Act
            var settings = new AmazonSettings();

            // Assert
            settings.DefaultRegion.Should().Be(RegionEndpoint.EUNorth1);
        }

        [Fact]
        public void AmazonSettings_ShouldInitializeSqsQueueName()
        {
            // Arrange
            var queueName = "test-queue";

            // Act
            var settings = new AmazonSettings { SqsQueueName = queueName };

            // Assert
            settings.SqsQueueName.Should().Be(queueName);
        }
    }
}
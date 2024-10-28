using Dotnetstore.OvenSimulator.Oven.Health;
using Dotnetstore.OvenSimulator.Oven.Services;
using Dotnetstore.OvenSimulator.SDK.Oven;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Dotnetstore.OvenSimulator.Oven.Tests.Health
{
    public class OvenHealthCheckTests
    {
        private readonly IOvenSimulator _ovenSimulator;
        private readonly ILogger<OvenHealthCheck> _logger;
        private readonly OvenHealthCheck _healthCheck;

        public OvenHealthCheckTests()
        {
            _ovenSimulator = Substitute.For<IOvenSimulator>();
            _logger = Substitute.For<ILogger<OvenHealthCheck>>();
            _healthCheck = new OvenHealthCheck(_ovenSimulator, _logger);
        }

        [Fact]
        public async Task CheckHealthAsync_ShouldReturnUnhealthy_WhenHeaterFailure()
        {
            // Arrange
            _ovenSimulator.CurrentError.Returns(OvenErrorType.HeaterFailure);

            // Act
            var result = await _healthCheck.CheckHealthAsync(new HealthCheckContext());

            // Assert
            Assert.Equal(HealthStatus.Unhealthy, result.Status);
            Assert.Equal("Heater failure", result.Description);
        }

        [Fact]
        public async Task CheckHealthAsync_ShouldReturnUnhealthy_WhenThermostatIssue()
        {
            // Arrange
            _ovenSimulator.CurrentError.Returns(OvenErrorType.ThermostatIssue);

            // Act
            var result = await _healthCheck.CheckHealthAsync(new HealthCheckContext());

            // Assert
            Assert.Equal(HealthStatus.Unhealthy, result.Status);
            Assert.Equal("Thermostat issue", result.Description);
        }

        [Fact]
        public async Task CheckHealthAsync_ShouldReturnDegraded_WhenGradualHeaterFailure()
        {
            // Arrange
            _ovenSimulator.CurrentError.Returns(OvenErrorType.GradualHeaterFailure);

            // Act
            var result = await _healthCheck.CheckHealthAsync(new HealthCheckContext());

            // Assert
            Assert.Equal(HealthStatus.Degraded, result.Status);
            Assert.Equal("Gradual heater failure", result.Description);
        }

        [Fact]
        public async Task CheckHealthAsync_ShouldReturnDegraded_WhenIntermittentSensorFailure()
        {
            // Arrange
            _ovenSimulator.CurrentError.Returns(OvenErrorType.IntermittentSensorFailure);

            // Act
            var result = await _healthCheck.CheckHealthAsync(new HealthCheckContext());

            // Assert
            Assert.Equal(HealthStatus.Degraded, result.Status);
            Assert.Equal("Intermittent sensor failure", result.Description);
        }
    }
}
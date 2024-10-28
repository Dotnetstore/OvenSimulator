using Dotnetstore.OvenSimulator.SDK.Oven;
using FluentAssertions;
using Xunit;

namespace Dotnetstore.OvenSimulator.SDK.Tests.Oven
{
    public class OvenErrorTypeTests
    {
        [Fact]
        public void OvenErrorType_ShouldHaveCorrectValues()
        {
            // Assert
            ((int)OvenErrorType.None).Should().Be(0);
            ((int)OvenErrorType.HeaterFailure).Should().Be(1);
            ((int)OvenErrorType.GradualHeaterFailure).Should().Be(2);
            ((int)OvenErrorType.IntermittentSensorFailure).Should().Be(3);
            ((int)OvenErrorType.ThermostatIssue).Should().Be(4);
        }

        [Fact]
        public void OvenErrorType_ShouldHaveCorrectNames()
        {
            // Assert
            OvenErrorType.None.ToString().Should().Be("None");
            OvenErrorType.HeaterFailure.ToString().Should().Be("HeaterFailure");
            OvenErrorType.GradualHeaterFailure.ToString().Should().Be("GradualHeaterFailure");
            OvenErrorType.IntermittentSensorFailure.ToString().Should().Be("IntermittentSensorFailure");
            OvenErrorType.ThermostatIssue.ToString().Should().Be("ThermostatIssue");
        }
    }
}
using Dotnetstore.OvenSimulator.SharedKernel.Services;
using FluentAssertions;
using Xunit;

namespace Dotnetstore.OvenSimulator.SharedKernel.Tests.Services
{
    public class DataSchemeConstantsTests
    {
        [Fact]
        public void DataSchemeConstants_ShouldHaveCorrectNameMaxLength()
        {
            // Assert
            DataSchemeConstants.NameMaxLength.Should().Be(50);
        }

        [Fact]
        public void DataSchemeConstants_ShouldHaveCorrectDefaultRecipeNameValue()
        {
            // Assert
            DataSchemeConstants.DefaultRecipeNameValue.Should().Be("Pizza");
        }

        [Fact]
        public void DataSchemeConstants_ShouldHaveCorrectDefaultRecipeIdValue()
        {
            // Assert
            DataSchemeConstants.DefaultRecipeIdValue.Should().Be("959E3C51-856D-478E-8CE7-1F69D0257AC3");
        }

        [Fact]
        public void DataSchemeConstants_ShouldHaveCorrectDefaultHeatCapacityValue()
        {
            // Assert
            DataSchemeConstants.DefaultHeatCapacityValue.Should().Be(5000.0);
        }

        [Fact]
        public void DataSchemeConstants_ShouldHaveCorrectDefaultHeatLossCoefficientValue()
        {
            // Assert
            DataSchemeConstants.DefaultHeatLossCoefficientValue.Should().Be(0.1);
        }

        [Fact]
        public void DataSchemeConstants_ShouldHaveCorrectDefaultHeaterPowerPercentageValue()
        {
            // Assert
            DataSchemeConstants.DefaultHeaterPowerPercentageValue.Should().Be(100.0);
        }

        [Fact]
        public void DataSchemeConstants_ShouldHaveCorrectDefaultTargetTemperatureValue()
        {
            // Assert
            DataSchemeConstants.DefaultTargetTemperatureValue.Should().Be(300.0);
        }
    }
}
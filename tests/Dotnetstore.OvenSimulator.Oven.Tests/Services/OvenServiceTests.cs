using Ardalis.Result;
using FluentAssertions;
using Dotnetstore.OvenSimulator.Oven.Services;
using Dotnetstore.OvenSimulator.SDK.Oven;
using Dotnetstore.OvenSimulator.SDK.Oven.Requests;
using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;
using FluentAssertions.Execution;
using NSubstitute;
using Xunit;

namespace Dotnetstore.OvenSimulator.Oven.Tests.Services
{
    public class OvenServiceTests
    {
        private readonly IOvenSimulator _ovenSimulator;
        private readonly IOvenService _ovenService;

        public OvenServiceTests()
        {
            _ovenSimulator = Substitute.For<IOvenSimulator>();
            _ovenService = new OvenService(_ovenSimulator);
        }

        [Fact]
        public void StartOven_ShouldReturnNotFound_WhenNoRecipeLoaded()
        {
            // Arrange
            RecipeResponse? recipe = null;
            _ovenSimulator.ActiveRecipe.Returns(recipe);

            // Act
            var result = _ovenService.StartOven();

            // Assert
            using (new AssertionScope())
            {
                result.Status.Should().Be(ResultStatus.NotFound);
                result.Errors.Should().Contain("No recipe is loaded. Load a recipe first.");
            }
        }

        [Fact]
        public void StartOven_ShouldReturnConflict_WhenOvenAlreadyHeating()
        {
            // Arrange
            _ovenSimulator.ActiveRecipe.Returns(new RecipeResponse());
            _ovenSimulator.HeatingElementOn.Returns(true);
        
            // Act
            var result = _ovenService.StartOven();
        
            // Assert
            using (new AssertionScope())
            {
                result.Status.Should().Be(ResultStatus.Conflict);
                result.Errors.Should().Contain("Oven is already heating");
            }
        }

        [Fact]
        public void StartOven_ShouldReturnSuccess_WhenOvenStartsSuccessfully()
        {
            // Arrange
            _ovenSimulator.ActiveRecipe.Returns(new RecipeResponse());
            _ovenSimulator.HeatingElementOn.Returns(false);
        
            // Act
            var result = _ovenService.StartOven();
        
            // Assert
            using (new AssertionScope())
            {
                result.Status.Should().Be(ResultStatus.Ok);
                _ovenSimulator.Received(1).StartHeating();
            }
        }

        [Fact]
        public void StopOven_ShouldReturnConflict_WhenOvenAlreadyStopped()
        {
            // Arrange
            _ovenSimulator.HeatingElementOn.Returns(false);
        
            // Act
            var result = _ovenService.StopOven();
        
            // Assert
            using (new AssertionScope())
            {
                result.Status.Should().Be(ResultStatus.Conflict);
                result.Errors.Should().Contain("Oven is already stopped");
            }
        }

        [Fact]
        public void StopOven_ShouldReturnSuccess_WhenOvenStopsSuccessfully()
        {
            // Arrange
            _ovenSimulator.HeatingElementOn.Returns(true);
        
            // Act
            var result = _ovenService.StopOven();
        
            // Assert
            using (new AssertionScope())
            {
                result.Status.Should().Be(ResultStatus.Ok);
                _ovenSimulator.Received(1).StopHeating();
            }
        }

        [Fact]
        public void AddError_ShouldSimulateError()
        {
            // Arrange
            const OvenErrorType errorType = OvenErrorType.HeaterFailure;
            var request = new AddErrorRequest { ErrorType = errorType };
        
            // Act
            _ovenService.AddError(request);
        
            // Assert
            _ovenSimulator.Received(1).SimulateError(errorType);
        }
    }
}
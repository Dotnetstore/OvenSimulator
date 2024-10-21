using Dotnetstore.OvenSimulator.SDK.Recipes.Requests;
using Dotnetstore.OvenSimulator.Recipes.Create;
using Dotnetstore.OvenSimulator.SharedKernel.Services;
using FluentValidation.TestHelper;
using Xunit;

namespace Dotnetstore.OvenSimulator.Recipes.Tests.Create;

public class CreateRecipeValidatorTests
{
    private readonly CreateRecipeValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var request = new CreateRecipeRequest(string.Empty, 500.0, 0.5, 50.0, 200.0);
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage("Name is required");
    }

    [Fact]
    public void Should_Have_Error_When_Name_Exceeds_Max_Length()
    {
        var request = new CreateRecipeRequest(new string('A', DataSchemeConstants.NameMaxLength + 1), 500.0, 0.5, 50.0, 200.0);
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.Name)
              .WithErrorMessage($"Name must be at most {DataSchemeConstants.NameMaxLength} characters long");
    }

    [Fact]
    public void Should_Have_Error_When_HeatCapacity_Is_Less_Than_100()
    {
        var request = new CreateRecipeRequest("Test Recipe", 99.0, 0.5, 50.0, 200.0);
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.HeatCapacity)
              .WithErrorMessage("Heat capacity must be at least 100.0");
    }

    [Fact]
    public void Should_Have_Error_When_HeatCapacity_Is_Greater_Than_10000()
    {
        var request = new CreateRecipeRequest("Test Recipe", 10001.0, 0.5, 50.0, 200.0);
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.HeatCapacity)
              .WithErrorMessage("Heat capacity must be at most 10000.0");
    }

    [Fact]
    public void Should_Have_Error_When_HeatLossCoefficient_Is_Less_Than_0()
    {
        var request = new CreateRecipeRequest("Test Recipe", 500.0, -0.1, 50.0, 200.0);
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.HeatLossCoefficient)
              .WithErrorMessage("Heat loss coefficient must be at least 0.0");
    }

    [Fact]
    public void Should_Have_Error_When_HeatLossCoefficient_Is_Greater_Than_1()
    {
        var request = new CreateRecipeRequest("Test Recipe", 500.0, 1.1, 50.0, 200.0);
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.HeatLossCoefficient)
              .WithErrorMessage("Heat loss coefficient must be at most 1.0");
    }

    [Fact]
    public void Should_Have_Error_When_HeaterPowerPercentage_Is_Less_Than_0()
    {
        var request = new CreateRecipeRequest("Test Recipe", 500.0, 0.5, -1.0, 200.0);
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.HeaterPowerPercentage)
              .WithErrorMessage("Heater power percentage must be at least 0.0");
    }

    [Fact]
    public void Should_Have_Error_When_HeaterPowerPercentage_Is_Greater_Than_100()
    {
        var request = new CreateRecipeRequest("Test Recipe", 500.0, 0.5, 101.0, 200.0);
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.HeaterPowerPercentage)
              .WithErrorMessage("Heater power percentage must be at most 100.0");
    }

    [Fact]
    public void Should_Have_Error_When_TargetTemperature_Is_Less_Than_Or_Equal_To_100()
    {
        var request = new CreateRecipeRequest("Test Recipe", 500.0, 0.5, 50.0, 100.0);
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.TargetTemperature)
              .WithErrorMessage("Target temperature must be greater than 100.0");
    }

    [Fact]
    public void Should_Have_Error_When_TargetTemperature_Is_Greater_Than_HeatCapacity()
    {
        var request = new CreateRecipeRequest("Test Recipe", 500.0, 0.5, 50.0, 600.0);
        var result = _validator.TestValidate(request);
        result.ShouldHaveValidationErrorFor(x => x.TargetTemperature)
              .WithErrorMessage("Target temperature must be at most the heat capacity");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Request_Is_Valid()
    {
        var request = new CreateRecipeRequest("Test Recipe", 500.0, 0.5, 50.0, 200.0);
        var result = _validator.TestValidate(request);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
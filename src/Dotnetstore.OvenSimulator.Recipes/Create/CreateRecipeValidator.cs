using Dotnetstore.OvenSimulator.SDK.Recipes.Requests;
using Dotnetstore.OvenSimulator.SharedKernel.Services;
using FastEndpoints;
using FluentValidation;

namespace Dotnetstore.OvenSimulator.Recipes.Create;

internal sealed class CreateRecipeValidator : Validator<CreateRecipeRequest>
{
    public CreateRecipeValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Name is required")
            .MaximumLength(DataSchemeConstants.NameMaxLength)
            .WithMessage($"Name must be at most {DataSchemeConstants.NameMaxLength} characters long");
        
        RuleFor(x => x.HeatCapacity)
            .GreaterThanOrEqualTo(100.0)
            .WithMessage("Heat capacity must be at least 100.0")
            .LessThanOrEqualTo(10000.0)
            .WithMessage("Heat capacity must be at most 10000.0");
        
        RuleFor(x => x.HeatLossCoefficient)
            .GreaterThanOrEqualTo(0.0)
            .WithMessage("Heat loss coefficient must be at least 0.0")
            .LessThanOrEqualTo(1.0)
            .WithMessage("Heat loss coefficient must be at most 1.0");
        
        RuleFor(x => x.HeaterPowerPercentage)
            .GreaterThanOrEqualTo(0.0)
            .WithMessage("Heater power percentage must be at least 0.0")
            .LessThanOrEqualTo(100.0)
            .WithMessage("Heater power percentage must be at most 100.0");
        
        RuleFor(x => x.TargetTemperature)
            .GreaterThan(100.0)
            .WithMessage("Target temperature must be greater than 100.0")
            .LessThanOrEqualTo(x => x.HeatCapacity)
            .WithMessage("Target temperature must be at most the heat capacity");
    }
}
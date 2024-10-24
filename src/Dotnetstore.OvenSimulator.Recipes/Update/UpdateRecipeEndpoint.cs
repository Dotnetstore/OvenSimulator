using Dotnetstore.OvenSimulator.Contracts.Queries;
using Dotnetstore.OvenSimulator.Recipes.Services;
using Dotnetstore.OvenSimulator.SDK;
using Dotnetstore.OvenSimulator.SDK.Recipes.Requests;
using Dotnetstore.OvenSimulator.SharedKernel.Services;
using FastEndpoints;
using FastEndpoints.Swagger;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Dotnetstore.OvenSimulator.Recipes.Update;

internal sealed class UpdateRecipeEndpoint(
    IRecipeService recipeService,
    ISender sender) : Endpoint<UpdateRecipeRequest>
{
    public override void Configure()
    {
        Put(ApiEndpoints.Recipe.Update);
        Summary(s =>
            s.ExampleRequest = new UpdateRecipeRequest
            {
                Id = Guid.Parse(DataSchemeConstants.DefaultRecipeIdValue),
                Name = DataSchemeConstants.DefaultRecipeNameValue,
                HeatCapacity = DataSchemeConstants.DefaultHeatCapacityValue,
                HeaterPowerPercentage = DataSchemeConstants.DefaultHeaterPowerPercentageValue,
                HeatLossCoefficient = DataSchemeConstants.DefaultHeatLossCoefficientValue,
                TargetTemperature = DataSchemeConstants.DefaultTargetTemperatureValue
            });
        Description(x =>
            x.WithDescription("Update a recipe")
                .AutoTagOverride("Recipes"));
        Roles("Operator");
    }

    public override async Task HandleAsync(UpdateRecipeRequest req, CancellationToken ct)
    {
        var result = await recipeService.UpdateAsync(req, ct);
        
        if (!result.IsSuccess)
        {
            AddError(string.Join(", ", result.Errors.Select(x => x)));
            await SendErrorsAsync(cancellation: ct);
        }
        else
        {
            if (result.Value is null)
            {
                AddError("Recipe was not updated");
                await SendErrorsAsync(cancellation: ct);
            }
            else
            {
                await SendOkAsync(cancellation: ct);
            
                var query = new UpdatedRecipeQuery(req, result.Value);
                _ = sender.Send(query, ct);
            }
        }
    }
}
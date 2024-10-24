using Dotnetstore.OvenSimulator.Contracts.Queries;
using Dotnetstore.OvenSimulator.Recipes.GetById;
using Dotnetstore.OvenSimulator.Recipes.Services;
using Dotnetstore.OvenSimulator.SDK;
using Dotnetstore.OvenSimulator.SDK.Recipes.Requests;
using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;
using Dotnetstore.OvenSimulator.SharedKernel.Services;
using FastEndpoints;
using FastEndpoints.Swagger;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Dotnetstore.OvenSimulator.Recipes.Create;

internal sealed class CreateRecipeEndpoint(
    IRecipeService recipeService,
    ISender sender) : Endpoint<CreateRecipeRequest, RecipeResponse?>
{
    public override void Configure()
    {
        Post(ApiEndpoints.Recipe.Create);
        Summary(s =>
            s.ExampleRequest = new CreateRecipeRequest
            {
                Name = DataSchemeConstants.DefaultRecipeNameValue,
                HeatCapacity = DataSchemeConstants.DefaultHeatCapacityValue,
                HeaterPowerPercentage = DataSchemeConstants.DefaultHeaterPowerPercentageValue,
                HeatLossCoefficient = DataSchemeConstants.DefaultHeatLossCoefficientValue,
                TargetTemperature = DataSchemeConstants.DefaultTargetTemperatureValue
            });
        Description(x =>
            x.WithDescription("Create a new recipe")
                .AutoTagOverride("Recipes"));
        Roles("Operator");
    }

    public override async Task HandleAsync(CreateRecipeRequest req, CancellationToken ct)
    {
        var result = await recipeService.CreateAsync(req, ct);
        
        if (!result.IsSuccess)
        {
            AddError(string.Join(", ", result.Errors.Select(x => x)));
            await SendErrorsAsync(cancellation: ct);
        }
        else
        {
            if (result.Value is null)
            {
                AddError("Recipe was not created");
                await SendErrorsAsync(cancellation: ct);
            }
            else
            {
                var recipeResponse = result.Value.Value;
                await SendCreatedAtAsync<GetRecipeByIdEndpoint>(new { id = recipeResponse.Id }, recipeResponse, cancellation: ct);
            
                var query = new CreatedRecipeQuery(req, recipeResponse);
                _ = sender.Send(query, ct);
            }
        }
    }
}
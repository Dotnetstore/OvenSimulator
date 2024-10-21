using Dotnetstore.OvenSimulator.Recipes.Services;
using Dotnetstore.OvenSimulator.SDK;
using Dotnetstore.OvenSimulator.SDK.Recipes.Requests;
using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http;

namespace Dotnetstore.OvenSimulator.Recipes.Create;

internal sealed class CreateRecipeEndpoint(IRecipeService recipeService) : Endpoint<CreateRecipeRequest, RecipeResponse?>
{
    public override void Configure()
    {
        Post(ApiEndpoints.Recipe.Create);
        Description(x =>
            x.WithDescription("Create a new recipe")
                .AutoTagOverride("Recipes"));
        AllowAnonymous();
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
            await SendAsync(result.Value, statusCode: 201, cancellation: ct);
    }
}
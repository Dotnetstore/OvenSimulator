using Dotnetstore.OvenSimulator.Recipes.Services;
using Dotnetstore.OvenSimulator.SDK;
using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http;

namespace Dotnetstore.OvenSimulator.Recipes.GetAll;

internal sealed class GetAllRecipeEndpoint(
    IRecipeService recipeService) : EndpointWithoutRequest<IEnumerable<RecipeResponse>>
{
    public override void Configure()
    {
        Get(ApiEndpoints.Recipe.GetAll);
        Description(x =>
            x.WithDescription("Get all recipes")
                .AutoTagOverride("Recipes"));
        Roles("Operator");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var recipes = await recipeService.GetAllAsync(ct);

        await SendAsync(recipes, statusCode: StatusCodes.Status200OK, ct);
    }
}
using Dotnetstore.OvenSimulator.Recipes.Services;
using Dotnetstore.OvenSimulator.SDK;
using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http;

namespace Dotnetstore.OvenSimulator.Recipes.GetById;

internal sealed class GetRecipeByIdEndpoint(IRecipeService recipeService) : EndpointWithoutRequest<RecipeResponse?>
{
    public override void Configure()
    {
        Get(ApiEndpoints.Recipe.GetById);
        Description(x =>
            x.WithDescription("Get recipe by id")
                .AutoTagOverride("Recipes"));
        Roles("Operator");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var result = await recipeService.GetByIdAsync(id, ct);
    
        if (!result.IsSuccess)
        {
            AddError(string.Join(", ", result.Errors.Select(x => x)));
            await SendErrorsAsync(cancellation: ct);
        }
        else
            await SendAsync(result.Value, statusCode: 200, cancellation: ct);
    }
}
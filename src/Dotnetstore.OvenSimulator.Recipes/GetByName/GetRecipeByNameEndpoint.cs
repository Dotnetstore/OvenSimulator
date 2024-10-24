using Dotnetstore.OvenSimulator.Recipes.Services;
using Dotnetstore.OvenSimulator.SDK;
using Dotnetstore.OvenSimulator.SDK.Recipes.Responses;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http;

namespace Dotnetstore.OvenSimulator.Recipes.GetByName;

internal sealed class GetRecipeByNameEndpoint(IRecipeService recipeService) : EndpointWithoutRequest<RecipeResponse?>
{
    public override void Configure()
    {
        Get(ApiEndpoints.Recipe.GetByName);
        Description(x =>
            x.WithDescription("Get recipe by name")
                .AutoTagOverride("Recipes"));
        Roles("Operator");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var name = Route<string>("name");
        
        if(string.IsNullOrWhiteSpace(name))
        {
            AddError("Name is required");
            await SendErrorsAsync(cancellation: ct);
            return;
        }
        
        var result = await recipeService.GetByNameAsync(name, ct);
    
        if (!result.IsSuccess)
        {
            AddError(string.Join(", ", result.Errors.Select(x => x)));
            await SendErrorsAsync(cancellation: ct);
        }
        else
            await SendAsync(result.Value, statusCode: 200, cancellation: ct);
    }
}
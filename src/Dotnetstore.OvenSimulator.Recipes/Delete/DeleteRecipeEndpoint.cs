using Dotnetstore.OvenSimulator.Contracts.Queries;
using Dotnetstore.OvenSimulator.Recipes.Services;
using Dotnetstore.OvenSimulator.SDK;
using FastEndpoints;
using FastEndpoints.Swagger;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Dotnetstore.OvenSimulator.Recipes.Delete;

internal sealed class DeleteRecipeEndpoint(
    IRecipeService recipeService,
    ISender sender) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Delete(ApiEndpoints.Recipe.Delete);
        Description(x =>
            x.WithDescription("Delete recipe by id")
                .AutoTagOverride("Recipes"));
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var id = Route<Guid>("id");
        var result = await recipeService.DeleteAsync(id, ct);
        
        if (!result.IsSuccess)
        {
            AddError(string.Join(", ", result.Errors.Select(x => x)));
            await SendErrorsAsync(cancellation: ct);
        }
        else
        {
            await SendNoContentAsync(ct);
            
            var query = new DeletedRecipeQuery(id);
            _ = sender.Send(query, ct);
        }
    }
}
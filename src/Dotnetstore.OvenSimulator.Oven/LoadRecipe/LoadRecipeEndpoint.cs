using Dotnetstore.OvenSimulator.Contracts.Commands;
using Dotnetstore.OvenSimulator.Oven.Services;
using Dotnetstore.OvenSimulator.SDK;
using FastEndpoints;
using FastEndpoints.Swagger;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Dotnetstore.OvenSimulator.Oven.LoadRecipe;

internal sealed class LoadRecipeEndpoint(
    IOvenSimulator ovenSimulator,
    ISender sender) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Get(ApiEndpoints.Oven.LoadRecipe);
        Description(x =>
            x.WithDescription("Load recipe by name")
                .AutoTagOverride("Oven"));
        Roles("Operator");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var name = Route<string>("name");
        
        if (string.IsNullOrWhiteSpace(name))
        {
            AddError("Name is required");
            await SendErrorsAsync(cancellation: ct);
            return;
        }
        
        var command = new GetRecipeByNameCommand(name);
        var result = await sender.Send(command, ct);
        
        if (!result.IsSuccess)
        {
            AddError(string.Join(", ", result.Errors.Select(x => x)));
            await SendErrorsAsync(cancellation: ct);
        }
        else
        {
            ovenSimulator.ActiveRecipe = result.Value;
            await SendOkAsync(ct);
        }
    }
}
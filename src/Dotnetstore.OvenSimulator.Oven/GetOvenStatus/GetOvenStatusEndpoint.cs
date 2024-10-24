using Dotnetstore.OvenSimulator.Oven.Services;
using Dotnetstore.OvenSimulator.SDK;
using Dotnetstore.OvenSimulator.SDK.Oven.Responses;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http;

namespace Dotnetstore.OvenSimulator.Oven.GetOvenStatus;

internal sealed class GetOvenStatusEndpoint(IOvenSimulator ovenSimulator) : EndpointWithoutRequest<GetOvenStatusResponse>
{
    public override void Configure()
    {
        Get(ApiEndpoints.Oven.Get);
        Description(x =>
            x.WithDescription("Get oven status")
                .AutoTagOverride("Oven"));
        Roles("Operator");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var response = new GetOvenStatusResponse(
            ovenSimulator.CurrentTemperature,
            ovenSimulator.HeatingElementOn,
            ovenSimulator.ActiveRecipe?.Name ?? "No active recipe",
            ovenSimulator.CurrentError.ToString());

        await SendAsync(response, cancellation: ct);
    }
}
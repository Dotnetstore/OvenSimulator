using Dotnetstore.OvenSimulator.Oven.Services;
using Dotnetstore.OvenSimulator.SDK;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http;

namespace Dotnetstore.OvenSimulator.Oven.StartOven;

internal sealed class StartOvenEndpoint(IOvenService ovenService) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post(ApiEndpoints.Oven.Start);
        Description(x =>
            x.WithDescription("Start oven")
                .AutoTagOverride("Oven"));
        Roles("Operator");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var result = ovenService.StartOven();

        if (result.IsSuccess)
            await SendOkAsync(ct);
        else
        {
            AddError(string.Join(", ", result.Errors));
            await SendErrorsAsync(cancellation: ct);
        }
    }
}
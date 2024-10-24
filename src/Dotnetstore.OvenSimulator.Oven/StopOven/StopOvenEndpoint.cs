using Dotnetstore.OvenSimulator.Oven.Services;
using Dotnetstore.OvenSimulator.SDK;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http;

namespace Dotnetstore.OvenSimulator.Oven.StopOven;

internal sealed class StopOvenEndpoint(IOvenService ovenService) : EndpointWithoutRequest
{
    public override void Configure()
    {
        Post(ApiEndpoints.Oven.Stop);
        Description(x =>
            x.WithDescription("Stop oven")
                .AutoTagOverride("Oven"));
        Roles("Operator");
    }

    public override Task HandleAsync(CancellationToken ct)
    {
        var result = ovenService.StopOven();

        if (result.IsSuccess)
            return SendOkAsync(ct);
        
        AddError(string.Join(", ", result.Errors));
        return SendErrorsAsync(cancellation: ct);
    }
}
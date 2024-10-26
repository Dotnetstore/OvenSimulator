using Dotnetstore.OvenSimulator.Contracts.Queries;
using Dotnetstore.OvenSimulator.Oven.Services;
using Dotnetstore.OvenSimulator.SDK;
using Dotnetstore.OvenSimulator.SDK.Oven;
using Dotnetstore.OvenSimulator.SDK.Oven.Requests;
using FastEndpoints;
using FastEndpoints.Swagger;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Dotnetstore.OvenSimulator.Oven.AddError;

internal sealed class AddErrorEndpoint(
    IOvenService ovenService,
    ISender sender) : Endpoint<AddErrorRequest>
{
    public override void Configure()
    {
        Post(ApiEndpoints.Oven.AddError);
        Summary(s =>
            s.ExampleRequest = new AddErrorRequest
            {
                ErrorType = OvenErrorType.None
            });
        Description(x =>
            x.WithDescription("Add error to the oven")
                .AutoTagOverride("Oven"));
        Roles("Operator");
    }

    public override async Task HandleAsync(AddErrorRequest req, CancellationToken ct)
    {
        ovenService.AddError(req);
        
        var query = new SendOvenErrorEmailQuery(req.ErrorType.ToString());
        _ = sender.Send(query, ct);
        
        await SendOkAsync(ct);
    }
}
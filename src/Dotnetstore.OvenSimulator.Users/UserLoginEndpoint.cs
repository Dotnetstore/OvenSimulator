using Dotnetstore.OvenSimulator.SDK;
using Dotnetstore.OvenSimulator.SDK.Users.Requests;
using FastEndpoints;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Http;

namespace Dotnetstore.OvenSimulator.Users;

internal sealed class UserLoginEndpoint(IUserService userService) : Endpoint<LoginRequest>
{
    public override void Configure()
    {
        Post(ApiEndpoints.User.Login);
        Summary(s =>
            s.ExampleRequest = new LoginRequest
            {
                Username = "test@test.com",
                Password = "test"
            });
        Description(x =>
            x.WithDescription("User login")
                .AutoTagOverride("Users"));
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var token = await userService.LoginAsync(req, ct);

        if (!token.IsSuccess)
        {
            AddError(string.Join(", ", token.Errors));
            await SendErrorsAsync(cancellation: ct);
        }
        else
        {
            await SendAsync(token.Value, cancellation: ct);
        }
    }
}
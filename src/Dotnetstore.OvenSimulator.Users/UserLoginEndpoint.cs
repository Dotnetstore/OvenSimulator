using Dotnetstore.OvenSimulator.SDK;
using Dotnetstore.OvenSimulator.SDK.Users.Requests;
using FastEndpoints;

namespace Dotnetstore.OvenSimulator.Users;

internal sealed class UserLoginEndpoint(IUserService userService) : Endpoint<LoginRequest>
{
    public override void Configure()
    {
        Post(ApiEndpoints.User.Login);
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
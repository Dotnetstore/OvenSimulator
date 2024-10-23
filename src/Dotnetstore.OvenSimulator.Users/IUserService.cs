using Ardalis.Result;
using Dotnetstore.OvenSimulator.SDK.Users.Requests;

namespace Dotnetstore.OvenSimulator.Users;

internal interface IUserService
{
    ValueTask<Result<string>> LoginAsync(LoginRequest request, CancellationToken ct);
}
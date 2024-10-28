using Ardalis.Result;
using Dotnetstore.OvenSimulator.Contracts.Entities;
using Dotnetstore.OvenSimulator.SDK.Users.Requests;
using Dotnetstore.OvenSimulator.SharedKernel.Services;
using FastEndpoints.Security;
using Microsoft.Extensions.Configuration;

namespace Dotnetstore.OvenSimulator.Users;

internal sealed class UserService : IUserService
{
    private readonly IAuthenticationService _authenticationService;
    private readonly OvenUser _user;
    
    public UserService(
        IConfiguration configuration,
        IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
        _user = new OvenUser
        {
            Id = new UserId(configuration.GetValue<Guid>("User:Id")),
            Name = configuration.GetValue<string>("User:Name")!,
            Username = configuration.GetValue<string>("User:Username")!,
            Password = configuration.GetValue<string>("User:Password")!
        };
        
        _user.Roles.Add(new Role
        {
            Name = "Operator",
            UserId = _user.Id
        });
    }
    
    async ValueTask<Result<string>> IUserService.LoginAsync(LoginRequest request, CancellationToken ct)
    {
        await Task.CompletedTask;
        
        if (request.Username != _user.Username || request.Password != _user.Password)
        {
            return Result<string>.Unauthorized("The supplied credentials are invalid!");
        }
        
        var token = _authenticationService.CreateJwtToken(_user.Username, _user.Roles.Select(x => x.Name).ToArray(), _user.Id.Value);
        return token;
    }
}
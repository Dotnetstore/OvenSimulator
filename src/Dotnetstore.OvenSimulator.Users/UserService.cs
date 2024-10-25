using Ardalis.Result;
using Dotnetstore.OvenSimulator.Contracts.Entities;
using Dotnetstore.OvenSimulator.SDK.Users.Requests;
using FastEndpoints.Security;
using Microsoft.Extensions.Configuration;

namespace Dotnetstore.OvenSimulator.Users;

internal sealed class UserService : IUserService
{
    private readonly OvenUser _user;
    
    public UserService(
        IConfiguration configuration)
    {
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
        
        var token = CreateJwtToken(_user);
        return token;
    }

    private string CreateJwtToken(OvenUser user)
    {
        var jwtToken = JwtBearer.CreateToken(
            o =>
            {
                o.SigningKey = "This is a secret key. It should not be spread to anyone. Keep it safe. I keep it here because it is a demo.";
                o.ExpireAt = DateTime.UtcNow.AddYears(15);
                o.User.Roles.Add(user.Roles.Select(x => x.Name).ToArray());
                o.User.Claims.Add(("UserName", user.Username));
                o.User["UserId"] = user.Id.Value.ToString();
            });

        return jwtToken;
    }
}
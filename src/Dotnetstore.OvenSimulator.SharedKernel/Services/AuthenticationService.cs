using FastEndpoints.Security;

namespace Dotnetstore.OvenSimulator.SharedKernel.Services;

public sealed class AuthenticationService : IAuthenticationService
{
    string IAuthenticationService.CreateJwtToken(string username, string[] roles, Guid userId)
    {
        return JwtBearer.CreateToken(
            o =>
            {
                o.SigningKey = "This is a secret key. It should not be spread to anyone. Keep it safe. I keep it here because it is a demo.";
                o.ExpireAt = DateTime.UtcNow.AddHours(8);
                o.User.Roles.Add(roles);
                o.User.Claims.Add(("UserName", username));
                o.User["UserId"] = userId.ToString();
            });
    }
}
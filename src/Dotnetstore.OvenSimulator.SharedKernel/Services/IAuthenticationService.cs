namespace Dotnetstore.OvenSimulator.SharedKernel.Services;

public interface IAuthenticationService
{
    string CreateJwtToken(string username, string[] roles, Guid userId);
}
namespace Dotnetstore.OvenSimulator.Contracts.Entities;

public sealed class OvenUser
{
    public required UserId Id { get; set; }
    
    public required string Name { get; set; }

    public required string Username { get; set; }

    public required string Password { get; set; }

    public List<Role> Roles { get; set; } = new();
}

public record struct UserId(Guid Value);
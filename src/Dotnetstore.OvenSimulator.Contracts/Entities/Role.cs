namespace Dotnetstore.OvenSimulator.Contracts.Entities;

public class Role
{
    public required string Name { get; set; }

    public required UserId UserId { get; set; }
}
namespace Dotnetstore.OvenSimulator.Email.Models;

public sealed class Smtp
{
    public const string Key = "Smtp";
    
    public required string Host { get; set; }

    public int Port { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public required string ErrorFrom { get; set; }

    public required string ErrorSender { get; set; }
    
    public required string ErrorTo { get; set; }
    
    public required string ErrorSubject { get; set; }
    
    public required string NoReplyFrom { get; set; }
}
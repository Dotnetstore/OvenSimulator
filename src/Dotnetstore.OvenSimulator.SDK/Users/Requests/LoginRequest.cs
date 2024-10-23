namespace Dotnetstore.OvenSimulator.SDK.Users.Requests;

public record struct LoginRequest(
    string Username,
    string Password);
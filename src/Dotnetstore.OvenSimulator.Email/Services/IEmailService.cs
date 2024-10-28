using FluentEmail.Core.Models;

namespace Dotnetstore.OvenSimulator.Email.Services;

internal interface IEmailService
{
    ValueTask<SendResponse> SendEmailAsync(string to, string subject, string body, CancellationToken cancellationToken);
}